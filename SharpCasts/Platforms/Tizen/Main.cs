using System;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;

namespace SharpCasts;

/// <summary>
/// The class that represents the initial access point for Tizen devices.
/// </summary>
class Program : MauiApplication
{
    /// <summary>
    /// Builds configuration for the application.
    /// </summary>
    /// <returns>The configured application.</returns>
	protected override MauiApp CreateMauiApp()
		=> MauiProgram.CreateMauiApp();

	/// <summary>
    /// The main entry-point for the application.
    /// </summary>
    /// <param name="args">Arguments for the application.</param>
	static void Main(string[] args)
	{
		var app = new Program();
		app.Run(args);
	}
}
