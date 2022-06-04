﻿using SharpCasts.Core.Validation;
using SharpCasts.Main.Models;
using SharpCasts.Main.Services.Users;
using SharpCasts.Main.Views;
using SharpCasts.Core.Validation.Inputs;

using CommunityToolkit.Mvvm.ComponentModel;

using System.Windows.Input;

namespace SharpCasts.Main.ViewModels;

/// <summary>
/// The viewmodel for the <see cref="RegisterPage"/> content page.
/// </summary>
public partial class RegisterPageViewmodel : BaseViewModel
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
    /// Initializes a new instance of the <see cref="RegisterPageViewmodel"/> class.
    /// </summary>
    /// <param name="userService">The service to register users with.</param>
    public RegisterPageViewmodel(IUserService userService)
    {
        this.userService = userService;
        this.validator = new Validator();

        this.ErrorMsg = string.Empty;
        this.RegisterCommand = new Command(this.RegisterAsync);
    }

    /// <summary>
    /// Gets the command to execute when the user submits the form.
    /// </summary>
    public ICommand RegisterCommand { get; }

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
        App.CurrentUser = this.userService.GetUserByCredentials(registeringUser);
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
