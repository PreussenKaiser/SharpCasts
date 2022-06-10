namespace SharpCasts.Audio.Platforms.Android.CurrentActivity;

/// <summary>
/// 
/// </summary>
public class CrossCurrentActivity
{
    /// <summary>
    /// 
    /// </summary>
    private static readonly Lazy<ICurrentActivity> implementation = 
        new Lazy<ICurrentActivity>(() => CreateCurrentActivity(), LazyThreadSafetyMode.PublicationOnly);

    /// <summary>
    /// Current settings to use.
    /// </summary>
    public static ICurrentActivity Current
    {
        get
        {
            ICurrentActivity ret = implementation.Value;

            if (ret is null)
                throw NotImplementedInReferenceAssembly();

            return ret;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private static ICurrentActivity CreateCurrentActivity()
    {
#if NETSTANDARD1_0 || NETSTANDARD2_0
        return null;
#else
        return new CurrentActivityImplementation();
#endif
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    internal static Exception NotImplementedInReferenceAssembly()
        => new NotImplementedException("This functionality is not implemented in the portable version of this assembly. You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");
}