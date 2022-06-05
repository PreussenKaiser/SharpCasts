namespace SharedMauiLib.Platforms.Android;

/// <summary>
/// 
/// </summary>
/// <param name="sender">The object which initiated the event.</param>
/// <param name="e">Arguments for the event handler.</param>
public delegate void StatusChangedEventHandler(object sender, EventArgs e);

/// <summary>
/// Executes when a track starts buffering.
/// </summary>
/// <param name="sender">The object which initiated the event.</param>
/// <param name="e">Arguments for the event handler.</param>
public delegate void BufferingEventHandler(object sender, EventArgs e);

/// <summary>
/// Executes when the current track's cover is reloaded.
/// </summary>
/// <param name="sender">The object which initiated the event.</param>
/// <param name="e">Arguments for the event.</param>
public delegate void CoverReloadedEventHandler(object sender, EventArgs e);

/// <summary>
/// Executes when a track starts playing.
/// </summary>
/// <param name="sender">The object which initiated the event.</param>
/// <param name="e">Arguments for the event handler.</param>
public delegate void PlayingEventHandler(object sender, EventArgs e);

/// <summary>
/// Executes when the currently playing tracked is changed.
/// </summary>
/// <param name="sender">The object which initiated the event.</param>
/// <param name="e">Arguments for the event handler.</param>
public delegate void PlayingChangedEventHandler(object sender, bool e);