using SharpCasts.Core.Validation;
using SharpCasts.Core.Validation.Inputs;
using SharpCasts.Main.Models;
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
    /// Determines if login form inputs are correct.
    /// </summary>
    private readonly Validator validator;

    /// <summary>
    /// The message to display when an input is incorrect.
    /// </summary>
    private string errorMsg;

    /// <summary>
    /// Initializes a new instance of the <see cref="LoginPageViewmodel"/> viewmodel.
    /// </summary>
    /// <param name="userService">The service to get user data with.</param>
    public LoginPageViewmodel(IUserService userService)
    {
        this.userService = userService;
        this.validator = new Validator();

        this.Title = "Login";
        this.ErrorMsg = string.Empty;
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
    /// Gets or sets the error message to display.
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
    private void Login()
    {
        User loggingInUser = new()
        {
            Name = this.Username,
            Password = this.Password
        };

        this.ValidateInputs(loggingInUser);

        if (!string.IsNullOrEmpty(this.ErrorMsg))
            return;

        User loggedInUser = this.userService.GetUserByCredentials(loggingInUser);
        App.CurrentUser = loggedInUser;
    }

    /// <summary>
    /// Sends the user to the register page.
    /// </summary>
    private async void Register()
        => await Shell.Current.GoToAsync(nameof(RegisterPage));

    /// <summary>
    /// Validates inputs in the form.
    /// </summary>
    private void ValidateInputs(User user)
        => this.ErrorMsg = this.validator
            .Reset()
            .AddInput(new TextInput("Username", this.Username, 32))
            .AddInput(new TextInput("Password", this.Password, 128))
            .AddInput(new UserInput(user, this.userService))
            .Validate();
}
