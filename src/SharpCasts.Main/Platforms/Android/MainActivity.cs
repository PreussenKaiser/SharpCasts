using SharpCasts.Audio.Platforms.Android;
using SharpCasts.Audio.Platforms.Android.CurrentActivity;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;

namespace SharpCasts.Main;

/// <summary>
/// Runs configuration for the Android application.
/// </summary>
[Activity(
    Theme = "@style/Maui.SplashTheme",
    MainLauncher = true, 
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity, IAudioActivity
{
    /// <summary>
    /// Not implemented.
    /// </summary>
    public event StatusChangedEventHandler StatusChanged;

    /// <summary>
    /// Not implemented
    /// </summary>
    public event CoverReloadedEventHandler CoverReloaded;

    /// <summary>
    /// Not implemented
    /// </summary>
    public event PlayingEventHandler Playing;

    /// <summary>
    /// Not implemented.
    /// </summary>
    public event BufferingEventHandler Buffering;

    /// <summary>
    /// 
    /// </summary>
    private MediaPlayerServiceConnection mediaPlayerServiceConnection;

    /// <summary>
    /// 
    /// </summary>
    public MediaPlayerServiceBinder Binder { get; set; }

    /// <summary>
    /// Initializes the Android application.
    /// Called when the application runs on Android.
    /// </summary>
    /// <param name="savedInstanceState"></param>
    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        CrossCurrentActivity.Current.Init(this, savedInstanceState);
        NotificationHelper.CreateNotificationChannel(this.ApplicationContext);

        if (this.mediaPlayerServiceConnection is null)
            this.InitializeMedia();
    }

    /// <summary>
    /// Initializes the appication's media player.
    /// </summary>
    private void InitializeMedia()
    {
        this.mediaPlayerServiceConnection = new MediaPlayerServiceConnection(this);
        Intent mediaPlayerServiceIntent = new(this.ApplicationContext, typeof(MediaPlayerService));

        _ = this.BindService(mediaPlayerServiceIntent, this.mediaPlayerServiceConnection, Bind.AutoCreate);
    }
}
