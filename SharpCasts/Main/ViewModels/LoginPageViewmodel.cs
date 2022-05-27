using SharpCasts.Main.Services.Users;
using SharpCasts.Main.Views;

using System.Windows.Input;

namespace SharpCasts.Main.ViewModels;

/// <summary>
/// The viewmodel for the <see cref="LoginPage"/> content page.
/// </summary>
public class LoginPageViewmodel : BaseViewModel
{
    /// <summary>
    /// The service to get user data with.
    /// </summary>
    private readonly IUserService userService;

    /// <summary>
    /// Initializes a new instance of the <see cref="LoginPageViewmodel"/> viewmodel.
    /// </summary>
    /// <param name="userService">The service to get user data with.</param>
    public LoginPageViewmodel(IUserService userService)
    {
        this.userService = userService;

        this.Title = "Login";
        this.LoginCommand = new Command(this.Login);
        this.RegisterCommand = new Command(this.Register);
    }

    /// <summary>
    /// Gets the command to execute when the user submits the form.
    /// </summary>
    public ICommand LoginCommand { get; }

    /// <summary>
    /// Gets the command to execute when the user opts to register.
    /// </summary>
    public ICommand RegisterCommand { get; }

    /// <summary>
    /// Gets or sets the entered username.
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Gets or sets the entered password.
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Verifies if the user entered the correct credentials and logs them in.
    /// </summary>
    private async void Login()
    {
    }

    /// <summary>
    /// Sends the user to the register page.
    /// </summary>
    private async void Register()
        => await Shell.Current.GoToAsync(nameof(RegisterPage));
}
