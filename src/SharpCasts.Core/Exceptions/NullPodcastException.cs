namespace SharpCasts.Core.Exceptions;

/// <summary>
/// Thrown when a retrieved podcast could not be found.
/// </summary>
public class NullPodcastException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NullPodcastException"/> class.
    /// </summary>
    /// <param name="msg"></param>
    public NullPodcastException(string msg)
        : base(msg)
    {
    }
}
