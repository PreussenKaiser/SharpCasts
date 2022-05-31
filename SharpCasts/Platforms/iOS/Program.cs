using UIKit;

namespace SharpCasts;

/// <summary>
/// The class that represents the main entry-point of the IOS application.
/// </summary>
public class Program
{
	/// <summary>
	/// This is the main entry-point for the IOS application.
	/// </summary>
	/// <param name="args">Arguments for the application.</param>
	static void Main(string[] args)
	{
		// if you want to use a different Application Delegate class from "AppDelegate"
		// you can specify it here.
		UIApplication.Main(args, null, typeof(AppDelegate));
	}
}
