using SharpCasts.Main.Configuration;
using SharpCasts.Main.Views;

using SharpCasts.Core.Models;
using SharpCasts.Core.Services;
using SharpCasts.Core.Validation;
using SharpCasts.Core.Validation.Inputs;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace SharpCasts.Main.ViewModels;

/// <summary>
/// The view model for the <see cref="LoginPage"/> content page.
/// </summary>
public partial class LoginPageViewModel : BaseViewModel
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
    [ObservableProperty]
    private string errorMsg;

    /// <summary>
    /// Initializes a new instance of the <see cref="LoginPageViewModel"/> class.
    /// </summary>
    /// <param name="userService">The service to get user data with.</param>
    public LoginPageViewModel(IUserService userService)
    {
        this.userService = userService;
        this.validator = new Validator();

        this.ErrorMsg = string.Empty;
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
    [ICommand]
    private async void LoginAsync()
    {
        User loggingInUser = new()
        {
            Name = this.Username,
            Password = this.Password
        };

        this.ValidateInputs(loggingInUser);

        if (!string.IsNullOrEmpty(this.ErrorMsg))
            return;

        // Set logged in user.
        User loggedInUser = this.userService.GetUserByCredentials(loggingInUser);
        Session.CurrentUser = loggedInUser;

        // Navigate to profile.
        await Shell.Current.GoToAsync("..");
    }

    /// <summary>
    /// Validates inputs in the login form.
    /// </summary>
    private void ValidateInputs(User user)
        => this.ErrorMsg = this.validator
            .Reset()
            .AddInput(new TextInput("Username", this.Username, 32))
            .AddInput(new TextInput("Password", this.Password, 128))
            .AddInput(new UserInput(user, this.userService))
            .Validate();
}
