namespace SharpCasts.Core.Exceptions;

/// <summary>
/// Thrown when a retrieved podcast could not be found.
/// </summary>
public class UnknownPodcastException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UnknownPodcastException"/> class.
    /// </summary>
    /// <param name="msg"></param>
    public UnknownPodcastException(string msg)
        : base(msg)
    {
    }
}
