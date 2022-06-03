using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using SharedMauiLib.Platforms.Android;
using SharedMauiLib.Platforms.Android.CurrentActivity;

namespace SharpCasts;

/// <summary>
/// 
/// </summary>
[Activity(
    Theme = "@style/Maui.SplashTheme",
    MainLauncher = true, 
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity, IAudioActivity
{
    /// <summary>
    /// 
    /// </summary>
    private MediaPlayerServiceConnection mediaPlayerServiceConnection;

    /// <summary>
    /// 
    /// </summary>
    public MediaPlayerServiceBinder Binder { get; set; }

    #region Events
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
    #endregion

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
    /// 
    /// </summary>
    private void InitializeMedia()
    {
        this.mediaPlayerServiceConnection = new MediaPlayerServiceConnection(this);
        var mediaPlayerServiceIntent = new Intent(this.ApplicationContext, typeof(MediaPlayerService));

        _ = this.BindService(mediaPlayerServiceIntent, this.mediaPlayerServiceConnection, Bind.AutoCreate);
    }
}
