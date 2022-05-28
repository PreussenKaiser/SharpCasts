using SharpCasts.Main.Models;
using SharpCasts.Main.Services.Users;
using SharpCasts.Main.Views;

using System.Windows.Input;

namespace SharpCasts.Main.ViewModels;

/// <summary>
/// The viewmodel for the <see cref="RegisterPage"/> content page.
/// </summary>
public class RegisterPageViewmodel : BaseViewModel
{
    /// <summary>
    /// The service to register users with.
    /// </summary>
    private readonly IUserService userService;

    /// <summary>
    /// The error message to display.
    /// </summary>
    private string errorMsg;

    /// <summary>
    /// Initializes a new instance of the <see cref="RegisterPageViewmodel"/> viewmodel.
    /// </summary>
    /// <param name="userService">The service to register users with.</param>
    public RegisterPageViewmodel(IUserService userService)
    {
        this.userService = userService;

        this.Title = "Register";
        this.ErrorMsg = string.Empty;
        this.RegisterCommand = new Command(this.Register);
    }

    /// <summary>
    /// Gets the command to execute when the user submits the form.
    /// </summary>
    public ICommand RegisterCommand { get; }

    /// <summary>
    /// Gets or sets the error message in the form.
    /// </summary>
    public string ErrorMsg
    {
        get => this.errorMsg;
        set
        {
            this.errorMsg = value;
            this.OnPropertyChanged(nameof(this.ErrorMsg));
        }
    }

    /// <summary>
    /// Gets or sets the user's username.
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Gets or sets the user's password.
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Gets or sets the form's re-entered password.
    /// </summary>
    public string PasswordRe { get; set; }

    /// <summary>
    /// Determine if the user can register.
    /// If so, a new account is created.
    /// </summary>
    private async void Register()
    {
        this.ValidateInputs();

        if (!string.IsNullOrEmpty(this.ErrorMsg))
            return;

        User newUser = new()
        {
            Name = this.Username,
            Password = this.Password
        };

        await this.userService.AddUser(newUser);

        // Logs the user in.
        App.CurrentUser = await this.userService.GetUser(newUser);
    }

    /// <summary>
    /// Validates form inputs.
    /// </summary>
    private void ValidateInputs()
    {
        if (string.IsNullOrEmpty(this.Username))
        {
            this.ErrorMsg = "Please enter a username";
        }
        else if (this.Username.Length > 32)
        {
            this.ErrorMsg = "Username character length must be below 32";
        }
        else if (string.IsNullOrEmpty(this.Password))
        {
            this.ErrorMsg = "Please enter a password";
        }
        else if (this.Password.Length > 128)
        {
            this.ErrorMsg = "Password character length must be below 128";
        }
        else if (this.Password != this.PasswordRe)
        {
            this.ErrorMsg = "Re-entered password does not match original";
        }
    }
}
