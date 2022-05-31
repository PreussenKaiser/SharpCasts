using UIKit;

namespace SharpCasts;

/// <summary>
/// The class that represents the main entry-point of the MacOS application.
/// </summary>
public class Program
{
	/// <summary>
	/// This is the main entry-point for the MacOS application.
	/// </summary>
	/// <param name="args">Arguments for the application.</param>
	static void Main(string[] args)
	{
		// If you want to use a different Application Delegate class from "AppDelegate"
		// you can specify it here.
		UIApplication.Main(args, null, typeof(AppDelegate));
	}
}
