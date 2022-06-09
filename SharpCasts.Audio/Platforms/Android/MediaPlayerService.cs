using Android.App;
using Android.Content;
using Android.Media;
using Android.Net;
using Android.Net.Wifi;
using Android.OS;
using Android.Media.Session;
using AndroidNet = Android.Net;
using Android.Graphics;

namespace SharpCasts.Audio.Platforms.Android;

/// <summary>
/// The class which represents the facade for Android's media service.
/// </summary>
[Service(Exported = true)]
[IntentFilter(new[]
    {
        ActionPlay, ActionPause,
        ActionStop, ActionTogglePlayback,
        ActionNext, ActionPrevious
    })]
public class MediaPlayerService : Service,
   AudioManager.IOnAudioFocusChangeListener,
   MediaPlayer.IOnBufferingUpdateListener,
   MediaPlayer.IOnCompletionListener,
   MediaPlayer.IOnErrorListener,
   MediaPlayer.IOnPreparedListener
{
    #region Actions
    /// <summary>
    /// 
    /// </summary>
    public const string ActionPlay = "com.xamarin.action.PLAY";

    /// <summary>
    /// 
    /// </summary>
    public const string ActionPause = "com.xamarin.action.PAUSE";

    /// <summary>
    /// 
    /// </summary>
    public const string ActionStop = "com.xamarin.action.STOP";

    /// <summary>
    /// 
    /// </summary>
    public const string ActionTogglePlayback = "com.xamarin.action.TOGGLEPLAYBACK";

    /// <summary>
    /// 
    /// </summary>
    public const string ActionNext = "com.xamarin.action.NEXT";

    /// <summary>
    /// 
    /// </summary>
    public const string ActionPrevious = "com.xamarin.action.PREVIOUS";
    #endregion
    /// <summary>
    /// 
    /// </summary>
    public MediaPlayer mediaPlayer;

    /// <summary>
    /// 
    /// </summary>
    private AudioManager audioManager;

    /// <summary>
    /// 
    /// </summary>
    private MediaSession mediaSession;

    /// <summary>
    /// 
    /// </summary>
    public MediaController mediaController;

    /// <summary>
    /// 
    /// </summary>
    private WifiManager wifiManager;

    /// <summary>
    /// 
    /// </summary>
    private WifiManager.WifiLock wifiLock;

    /// <summary>
    /// 
    /// </summary>
    public event StatusChangedEventHandler StatusChanged;

    /// <summary>
    /// 
    /// </summary>
    public event CoverReloadedEventHandler CoverReloaded;

    /// <summary>
    /// 
    /// </summary>
    public event PlayingEventHandler Playing;

    /// <summary>
    /// 
    /// </summary>
    public event BufferingEventHandler Buffering;

    /// <summary>
    /// 
    /// </summary>
    public event PlayingChangedEventHandler PlayingChanged;

    /// <summary>
    /// 
    /// </summary>
    public string AudioUrl;

    /// <summary>
    /// 
    /// </summary>
    public bool isCurrentEpisode = true;

    /// <summary>
    /// 
    /// </summary>
    private readonly Handler PlayingHandler;

    /// <summary>
    /// 
    /// </summary>
    private readonly Java.Lang.Runnable PlayingHandlerRunnable;

    /// <summary>
    /// 
    /// </summary>
    private ComponentName remoteComponentName;

    /// <summary>
    /// 
    /// </summary>
    private int buffered = 0;

    /// <summary>
    /// 
    /// </summary>
    private Bitmap cover;

    /// <summary>
    /// 
    /// </summary>
    private IBinder binder;

    /// <summary>
    /// Initializes a new instance of the <see cref="MediaPlayerService"/> class.
    /// </summary>
    public MediaPlayerService()
    {
        this.PlayingHandler = new Handler(Looper.MainLooper);

        // Create a runnable, restarting itself if the status still is "playing".
        this.PlayingHandlerRunnable = new Java.Lang.Runnable(() =>
        {
            OnPlaying(EventArgs.Empty);

            if (this.MediaPlayerState == PlaybackStateCode.Playing)
                this.PlayingHandler.PostDelayed(PlayingHandlerRunnable, 250);
        });

        // On Status changed to PLAYING, start raising the Playing event
        this.StatusChanged += (sender, e) =>
        {
            if (this.MediaPlayerState is PlaybackStateCode.Playing)
                this.PlayingHandler.PostDelayed(PlayingHandlerRunnable, 0);
        };
    }

    /// <summary>
    /// Gets the media player's current state.
    /// </summary>
    public PlaybackStateCode MediaPlayerState
        => this.mediaController.PlaybackState is not null
            ? this.mediaController.PlaybackState.State
            : PlaybackStateCode.None;

    /// <summary>
    /// Gets the media's duration.
    /// </summary>
    public int Duration
    {
        get
        {
            if (this.mediaPlayer is null
                || this.MediaPlayerState != PlaybackStateCode.Playing
                && this.MediaPlayerState != PlaybackStateCode.Paused)
            {
                return 0;
            }

            return this.mediaPlayer.Duration;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public int Buffered
    {
        get
        {
            return this.mediaPlayer is null
                ? 0
                : this.buffered;
        }
        private set
        {
            this.buffered = value;
            this.OnBuffering(EventArgs.Empty);
        }
    }

    /// <summary>
    /// Gets the current track's cover image.
    /// </summary>
    public object Cover
    {
        get
        {
            if (this.cover is null)
                this.cover = BitmapFactory.DecodeResource(
                    this.Resources,
                    Resource.Drawable.abc_ab_share_pack_mtrl_alpha);

            return this.cover;
        }
        private set
        {
            this.cover = value as Bitmap;
            this.OnCoverReloaded(EventArgs.Empty);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="e"></param>
    protected virtual void OnStatusChanged(EventArgs e)
        => this.StatusChanged?.Invoke(this, e);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="e"></param>
    protected virtual void OnPlayingChanged(bool e)
        => this.PlayingChanged?.Invoke(this, e);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="e"></param>
    protected virtual void OnCoverReloaded(EventArgs e)
    {
        if (this.CoverReloaded is not null)
        {
            this.CoverReloaded(this, e);

            this.StartNotification();
            this.UpdateMediaMetadataCompat();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="e"></param>
    protected virtual void OnPlaying(EventArgs e)
        => this.Playing?.Invoke(this, e);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="e"></param>
    protected virtual void OnBuffering(EventArgs e)
        => this.Buffering?.Invoke(this, e);

    /// <summary>
    /// On create, simply detect some of our managers.
    /// </summary>
    public override void OnCreate()
    {
        base.OnCreate();

        // Find our audio and notificaton managers.
        this.audioManager = (AudioManager)GetSystemService(AudioService);
        this.wifiManager = (WifiManager)GetSystemService(WifiService);

        this.remoteComponentName = new ComponentName(
            this.PackageName,
            new RemoteControlBroadcastReceiver().ComponentName);
    }

    /// <summary>
    /// Will register for the remote control client commands in audio manager.
    /// </summary>
    private void InitMediaSession()
    {
        try
        {
            if (this.mediaSession is null)
            {
                Intent nIntent = new(this.ApplicationContext, typeof(Activity));

                this.remoteComponentName = new ComponentName(
                    this.PackageName,
                    new RemoteControlBroadcastReceiver().ComponentName);

                this.mediaSession = new MediaSession(
                    this.ApplicationContext,
                    "MauiStreamingAudio");

                this.mediaSession.SetSessionActivity(
                    PendingIntent.GetActivity(
                        this.ApplicationContext, 0,
                        nIntent, PendingIntentFlags.Mutable));

                this.mediaController = new MediaController(
                    this.ApplicationContext,
                    this.mediaSession.SessionToken);
            }

            this.mediaSession.Active = true;
            this.mediaSession.SetCallback(
                new MediaSessionCallback((MediaPlayerServiceBinder)this.binder));

            this.mediaSession.SetFlags(
                MediaSessionFlags.HandlesMediaButtons |
                MediaSessionFlags.HandlesTransportControls);
        }
        catch (Exception ex)
        {
            // Log error.
            Console.WriteLine(ex);
        }
    }

    /// <summary>
    /// Intializes the media player.
    /// </summary>
    private void InitializePlayer()
    {
        this.mediaPlayer = new MediaPlayer();

        this.mediaPlayer.SetAudioAttributes(
            new AudioAttributes.Builder()
                .SetContentType(AudioContentType.Music)
                .SetUsage(AudioUsageKind.Media)
                .Build());

        this.mediaPlayer.SetWakeMode(ApplicationContext, WakeLockFlags.Partial);

        this.mediaPlayer.SetOnBufferingUpdateListener(this);
        this.mediaPlayer.SetOnCompletionListener(this);
        this.mediaPlayer.SetOnErrorListener(this);
        this.mediaPlayer.SetOnPreparedListener(this);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mp"></param>
    /// <param name="percent"></param>
    public void OnBufferingUpdate(MediaPlayer mp, int percent)
    {
        int duration = 0;

        if (this.MediaPlayerState == PlaybackStateCode.Playing 
            || this.MediaPlayerState == PlaybackStateCode.Paused)
        {
            duration = mp.Duration;
        }

        int newBufferedTime = duration * percent / 100;

        if (newBufferedTime != this.Buffered)
        {
            this.Buffered = newBufferedTime;
        }
    }

    /// <summary>
    /// Plays the next track when the current one is finished.
    /// </summary>
    /// <param name="mediaPlayer">The media player to play the next track with.</param>
    public async void OnCompletion(MediaPlayer mediaPlayer)
        => await this.PlayNext();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mediaPlayer"></param>
    /// <param name="what"></param>
    /// <param name="extra"></param>
    /// <returns></returns>
    public bool OnError(MediaPlayer mediaPlayer, MediaError what, int extra)
    {
        this.UpdatePlaybackState(PlaybackStateCode.Error);

        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mediaPlayer"></param>
    public void OnPrepared(MediaPlayer mediaPlayer)
    {
        mediaPlayer.Start();
        this.UpdatePlaybackState(PlaybackStateCode.Playing);
    }

    /// <summary>
    /// Gets the current position on in the audio track.
    /// </summary>
    public int Position
    {
        get
        {
            if (this.mediaPlayer is null
                || this.MediaPlayerState != PlaybackStateCode.Playing
                && this.MediaPlayerState != PlaybackStateCode.Paused)
            {
                return -1;
            }
            else
            {
                return this.mediaPlayer.CurrentPosition;
            }
        }
    }

    /// <summary>
    /// Plays or continues playing media.
    /// </summary>
    /// <returns>Whether the task was completed or not.</returns>
    public async Task Play()
    {
        if (this.mediaPlayer is not null && this.MediaPlayerState == PlaybackStateCode.Paused)
        {
            // We are simply paused so just start again.
            this.mediaPlayer.Start();
            this.UpdatePlaybackState(PlaybackStateCode.Playing);
            this.StartNotification();

            // Update the metadata now that we are playing
            this.UpdateMediaMetadataCompat();

            return;
        }

        if (this.mediaPlayer is null)
            this.InitializePlayer();

        if (this.mediaSession is null)
            this.InitMediaSession();

        if (this.mediaPlayer.IsPlaying && this.isCurrentEpisode)
        {
            this.UpdatePlaybackState(PlaybackStateCode.Playing);

            return;
        }

        this.isCurrentEpisode = true;

        await this.PrepareAndPlayMediaPlayerAsync();
    }

    /// <summary>
    /// Prepares the media player and plays it's audio.
    /// </summary>
    /// <returns>Whether the task was completed or not.</returns>
    private async Task PrepareAndPlayMediaPlayerAsync()
    {
        try
        {
            if (OperatingSystem.IsAndroidVersionAtLeast(21))
            {
                MediaMetadataRetriever metaRetriever = new();

                await this.mediaPlayer.SetDataSourceAsync(this.ApplicationContext, AndroidNet.Uri.Parse(AudioUrl));
                await metaRetriever.SetDataSourceAsync(this.AudioUrl, new Dictionary<string, string>());

                var focusResult = this.audioManager
                        .RequestAudioFocus(new AudioFocusRequestClass
                        .Builder(AudioFocus.Gain)
                        .SetOnAudioFocusChangeListener(this)
                        .Build());

                if (focusResult != AudioFocusRequest.Granted)
                    Console.WriteLine("Could not get audio focus");

                this.UpdatePlaybackState(PlaybackStateCode.Buffering);
                this.mediaPlayer.PrepareAsync();

                this.AquireWifiLock();
                this.UpdateMediaMetadataCompat(metaRetriever);
                this.StartNotification();

                byte[] imageByteArray = metaRetriever.GetEmbeddedPicture();

                this.Cover = imageByteArray is null
                    ? await BitmapFactory.DecodeResourceAsync(this.Resources, Resource.Drawable.abc_ab_share_pack_mtrl_alpha)
                    : await BitmapFactory.DecodeByteArrayAsync(imageByteArray, 0, imageByteArray.Length);
            }
        }
        catch (Exception ex)
        {
            this.UpdatePlaybackStateStopped();

            // Log error.
            Console.WriteLine(ex);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public async Task Seek(int position)
        => await Task.Run(() =>
        {
            if (this.mediaPlayer is not null)
                this.mediaPlayer.SeekTo(position);
        });

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async Task PlayNext()
    {
        if (this.mediaPlayer is not null)
        {
            this.mediaPlayer.Reset();
            this.mediaPlayer.Release();
            this.mediaPlayer = null;
        }

        this.UpdatePlaybackState(PlaybackStateCode.SkippingToNext);

        await this.Play();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async Task PlayPrevious()
    {
        // Start current track from beginning if it's the first track or the track has played more than 3sec and you hit "playPrevious".
        if (this.Position > 3000)
        {
            await this.Seek(0);
        }
        else
        {
            if (this.mediaPlayer is not null)
            {
                this.mediaPlayer.Reset();
                this.mediaPlayer.Release();
                this.mediaPlayer = null;
            }

            this.UpdatePlaybackState(PlaybackStateCode.SkippingToPrevious);

            await this.Play();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async Task PlayPause()
    {
        if (this.mediaPlayer is null 
            || this.mediaPlayer is not null 
            && this.MediaPlayerState == PlaybackStateCode.Paused)
        {
            await this.Play();
        }
        else
        {
            await this.Pause();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async Task Pause()
        => await Task.Run(() =>
        {
            if (this.mediaPlayer is null)
                return;

            if (this.mediaPlayer.IsPlaying)
            {
                this.mediaPlayer.Pause();
            }

            this.UpdatePlaybackState(PlaybackStateCode.Paused);
        });

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async Task Stop()
        => await Task.Run(() =>
        {
            if (this.mediaPlayer is null)
                return;

            if (this.mediaPlayer.IsPlaying)
            {
                this.mediaPlayer.Stop();
            }

            this.UpdatePlaybackState(PlaybackStateCode.Stopped);
            this.mediaPlayer.Reset();

            NotificationHelper.StopNotification(ApplicationContext);
            this.StopForeground(true);

            this.ReleaseWifiLock();
            this.UnregisterMediaSessionCompat();
        });

    /// <summary>
    /// 
    /// </summary>
    public void UpdatePlaybackStateStopped()
    {
        this.UpdatePlaybackState(PlaybackStateCode.Stopped);

        if (this.mediaPlayer is not null)
        {
            this.mediaPlayer.Reset();
            this.mediaPlayer.Release();

            this.mediaPlayer = null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="state"></param>
    private void UpdatePlaybackState(PlaybackStateCode state)
    {
        if (this.mediaSession is null || this.mediaPlayer is null)
            return;

        try
        {
            PlaybackState.Builder stateBuilder = new PlaybackState.Builder()
                .SetActions(
                    PlaybackState.ActionPause |
                    PlaybackState.ActionPlay |
                    PlaybackState.ActionPlayPause |
                    PlaybackState.ActionSkipToNext |
                    PlaybackState.ActionSkipToPrevious |
                    PlaybackState.ActionStop
                )
                .SetState(state, this.Position, 1.0f, SystemClock.ElapsedRealtime());

           this.mediaSession.SetPlaybackState(stateBuilder.Build());
           this.OnStatusChanged(EventArgs.Empty);

            if (state == PlaybackStateCode.Playing 
                || state == PlaybackStateCode.Paused)
            {
                this.StartNotification();
            }
        }
        catch (Exception ex)
        {
            // Log error.
            Console.WriteLine(ex);
        }
    }

    /// <summary>
    /// Initializes the playback controls notification.
    /// </summary>
    private void StartNotification()
    {
        if (this.mediaSession is null)
            return;

        NotificationHelper.StartNotification(
            this.ApplicationContext,
            this.mediaController.Metadata,
            this.mediaSession,
            this.Cover,
            this.MediaPlayerState == PlaybackStateCode.Playing);
    }

    /// <summary>
    /// Mutes the media player.
    /// </summary>
    /// <param name="value"></param>
    internal void SetMuted(bool value)
        => this.mediaPlayer.SetVolume(0, 0);

    /// <summary>
    /// Sets the volume of the media player.
    /// </summary>
    /// <param name="value">The volume to set.</param>
    internal void SetVolume(int value)
        => this.mediaPlayer.SetVolume(value, value);

    /// <summary>
    /// Updates the metadata on the lock screen.
    /// </summary>
    private void UpdateMediaMetadataCompat(MediaMetadataRetriever metaRetriever = null)
    {
        if (mediaSession is null)
            return;

        MediaMetadata.Builder builder = new();

        if (metaRetriever is not null)
        {
            builder
                .PutString(MediaMetadata.MetadataKeyAlbum, metaRetriever.ExtractMetadata(MetadataKey.Album))
                .PutString(MediaMetadata.MetadataKeyArtist, metaRetriever.ExtractMetadata(MetadataKey.Artist))
                .PutString(MediaMetadata.MetadataKeyTitle, metaRetriever.ExtractMetadata(MetadataKey.Title));
        }
        else
        {
            builder
                .PutString(MediaMetadata.MetadataKeyAlbum, this.mediaSession.Controller.Metadata.GetString(MediaMetadata.MetadataKeyAlbum))
                .PutString(MediaMetadata.MetadataKeyArtist, this.mediaSession.Controller.Metadata.GetString(MediaMetadata.MetadataKeyArtist))
                .PutString(MediaMetadata.MetadataKeyTitle, this.mediaSession.Controller.Metadata.GetString(MediaMetadata.MetadataKeyTitle));
        }

        builder.PutBitmap(MediaMetadata.MetadataKeyAlbumArt, this.Cover as Bitmap);
        this.mediaSession.SetMetadata(builder.Build());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="intent"></param>
    /// <param name="flags"></param>
    /// <param name="startId"></param>
    /// <returns></returns>
    public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
    {
        this.HandleIntent(intent);

        return base.OnStartCommand(intent, flags, startId);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="intent"></param>
    private void HandleIntent(Intent intent)
    {
        if (intent is null || intent.Action is null)
            return;

        string action = intent.Action;

        if (action.Equals(ActionPlay))
        {
            this.mediaController.GetTransportControls()
                                .Play();
        }
        else if (action.Equals(ActionPause))
        {
            this.mediaController.GetTransportControls()
                                .Pause();
        }
        else if (action.Equals(ActionPrevious))
        {
            this.mediaController.GetTransportControls()
                                .SkipToPrevious();
        }
        else if (action.Equals(ActionNext))
        {
            this.mediaController.GetTransportControls()
                                .SkipToNext();
        }
        else if (action.Equals(ActionStop))
        {
            this.mediaController.GetTransportControls()
                                .Stop();
        }
    }

    /// <summary>
    /// Lock the wifi so we can still stream under lock screen.
    /// </summary>
    private void AquireWifiLock()
    {
        if (this.wifiLock is null)
        {
            this.wifiLock = this.wifiManager.CreateWifiLock(
                WifiMode.Full, "xamarin_wifi_lock");
        }

        this.wifiLock.Acquire();
    }

    /// <summary>
    /// This will release the wifi lock if it is no longer needed.
    /// </summary>
    private void ReleaseWifiLock()
    {
        if (this.wifiLock is null)
            return;

        this.wifiLock.Release();
        this.wifiLock = null;
    }

    /// <summary>
    /// 
    /// </summary>
    private void UnregisterMediaSessionCompat()
    {
        try
        {
            if (this.mediaSession is not null)
            {
                this.mediaSession.Dispose();
                this.mediaSession = null;
            }
        }
        catch (Exception ex)
        {
            // Log error.
            Console.WriteLine(ex);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="intent"></param>
    /// <returns></returns>
    public override IBinder OnBind(Intent intent)
    {
        this.binder = new MediaPlayerServiceBinder(this);

        return binder;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="intent"></param>
    /// <returns></returns>
    public override bool OnUnbind(Intent intent)
    {
        NotificationHelper.StopNotification(this.ApplicationContext);

        return base.OnUnbind(intent);
    }

    /// <summary>
    /// Properly cleanup of your player by releasing resources.
    /// </summary>
    public override void OnDestroy()
    {
        base.OnDestroy();

        if (this.mediaPlayer is not null)
        {
            this.mediaPlayer.Release();
            this.mediaPlayer = null;

            NotificationHelper.StopNotification(this.ApplicationContext);
            this.StopForeground(true);

            this.ReleaseWifiLock();
            this.UnregisterMediaSessionCompat();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="focusChange"></param>
    public async void OnAudioFocusChange(AudioFocus focusChange)
    {
        switch (focusChange)
        {
            case AudioFocus.Gain:
                if (this.mediaPlayer is null)
                {
                    this.InitializePlayer();
                }

                if (!this.mediaPlayer.IsPlaying)
                {
                    this.mediaPlayer.Start();
                }

                this.mediaPlayer.SetVolume(1.0f, 1.0f);

                break;

            case AudioFocus.Loss:
                // We have lost focus stop!
                await Stop();

                break;

            case AudioFocus.LossTransient:
                // We have lost focus for a short time, but likely to resume so pause.
                await Pause();

                break;

            case AudioFocus.LossTransientCanDuck:
                // We have lost focus but should till play at a muted 10% volume
                if (this.mediaPlayer.IsPlaying)
                {
                    this.mediaPlayer.SetVolume(.1f, .1f);
                }

                break;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class MediaSessionCallback : MediaSession.Callback
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly MediaPlayerServiceBinder mediaPlayerService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaSessionCallback"/> class.
        /// </summary>
        /// <param name="service"></param>
        public MediaSessionCallback(MediaPlayerServiceBinder service)
            => this.mediaPlayerService = service;

        /// <summary>
        /// 
        /// </summary>
        public override void OnPause()
        {
            this.mediaPlayerService.GetMediaPlayerService()
                                   .OnPlayingChanged(false);

            base.OnPause();
        }

        /// <summary>
        /// 
        /// </summary>
        public override void OnPlay()
        {
            this.mediaPlayerService.GetMediaPlayerService()
                                   .OnPlayingChanged(true);

            base.OnPlay();
        }

        /// <summary>
        /// 
        /// </summary>
        public override async void OnSkipToNext()
        {
            await this.mediaPlayerService.GetMediaPlayerService()
                                         .PlayNext();

            base.OnSkipToNext();
        }

        /// <summary>
        /// 
        /// </summary>
        public override async void OnSkipToPrevious()
        {
            await this.mediaPlayerService.GetMediaPlayerService()
                                         .PlayPrevious();

            base.OnSkipToPrevious();
        }

        /// <summary>
        /// 
        /// </summary>
        public override async void OnStop()
        {
            await this.mediaPlayerService.GetMediaPlayerService()
                                         .Stop();

            base.OnStop();
        }
    }
}
