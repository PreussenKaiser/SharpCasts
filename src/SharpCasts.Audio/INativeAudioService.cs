namespace SharpCasts.Audio;

/// <summary>
/// THe interface that implements native audio service methods.
/// </summary>
public interface INativeAudioService
{
    /// <summary>
    /// The event handler for when the currently playing audio is changed.
    /// </summary>
    public event EventHandler<bool> IsPlayingChanged;

    /// <summary>
    /// Gets whether or not audio is playing.
    /// </summary>
    public bool IsPlaying { get; }

    /// <summary>
    /// Gets the current position in the audio's timeline.
    /// </summary>
    public double CurrentPosition { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="audioURI"></param>
    /// <returns></returns>
    public Task InitializeAsync(string audioURI);

    /// <summary>
    /// PLays the audio asynchronously.
    /// </summary>
    /// <param name="position">The position in the audio's timeline to begin play.</param>
    /// <returns>Whether the task was completed or not.</returns>
    public Task PlayAsync(double position = 0);

    /// <summary>
    /// Pauses the audio asynchronously.
    /// </summary>
    /// <returns>Whether the task was completed or not.</returns>
    public Task PauseAsync();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public Task SetMuted(bool value);

    /// <summary>
    /// Sets the audio's volume.
    /// </summary>
    /// <param name="value">The volume to set the volume to.</param>
    /// <returns>Whether the task was completed or not.</returns>
    public Task SetVolume(int value);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public Task SetCurrentTime(double value);

    /// <summary>
    /// Disposes the <see cref="INativeAudioService"/> implement.
    /// </summary>
    /// <returns>Whether the task was completed or not.</returns>
    public ValueTask DisposeAsync();
}