namespace SharedMauiLib.Platforms.Android;

/// <summary>
/// 
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
public delegate void StatusChangedEventHandler(object sender, EventArgs e);

/// <summary>
/// 
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
public delegate void BufferingEventHandler(object sender, EventArgs e);

/// <summary>
/// 
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
public delegate void CoverReloadedEventHandler(object sender, EventArgs e);

/// <summary>
/// 
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
public delegate void PlayingEventHandler(object sender, EventArgs e);

/// <summary>
/// 
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
public delegate void PlayingChangedEventHandler(object sender, bool e);