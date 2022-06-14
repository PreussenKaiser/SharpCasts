using SharpCasts.Main.Views;

using SharpCasts.Core.Models;
using SharpCasts.Core.Services;
using SharpCasts.Core.Validation;
using SharpCasts.Core.Validation.Inputs;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SharpCasts.Main.Helpers;

namespace SharpCasts.Main.ViewModels;

/// <summary>
/// View model for the <see cref="RegisterPage"/> content page.
/// </summary>
public partial class RegisterPageViewModel : BaseViewModel
{
    /// <summary>
    /// The service to register users with.
    /// </summary>
    private readonly IUserService userService;

    /// <summary>
    /// Validates inputs from the register form.
    /// </summary>
    private readonly Validator validator;

    /// <summary>
    /// The error message to display.
    /// </summary>
    [ObservableProperty]
    private string errorMsg;

    /// <summary>
    /// Initializes a new instance of the <see cref="RegisterPageViewModel"/> class.
    /// </summary>
    /// <param name="userService">The service to register users with.</param>
    public RegisterPageViewModel(IUserService userService)
    {
        this.userService = userService;
        this.validator = new Validator();

        this.ErrorMsg = string.Empty;
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
    [ICommand]
    private async void RegisterAsync()
    {
        this.ValidateInputs();

        if (!string.IsNullOrEmpty(this.ErrorMsg))
            return;

        User registeringUser = new()
        {
            Name = this.Username,
            Password = this.Password
        };

        await this.userService.AddUser(registeringUser);

        // Logs the user in.
        Settings.CurrentUser = this.userService.GetUserByCredentials(registeringUser);
    }

    /// <summary>
    /// Validates inputs in the register form.
    /// </summary>
    private void ValidateInputs()
        => this.ErrorMsg = this.validator
            .Reset()
            .AddInput(new TextInput("Username", this.Username, 32))
            .AddInput(new TextInput("Password", this.Password, 128))
            .Validate();
}
