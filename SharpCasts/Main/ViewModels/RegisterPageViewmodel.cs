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
    /// Initializes a new instance of the <see cref="RegisterPageViewmodel"/> viewmodel.
    /// </summary>
    /// <param name="userService">The service to register users with.</param>
    public RegisterPageViewmodel(IUserService userService)
    {
        this.userService = userService;

        this.Title = "Register";
        this.RegisterCommand = new Command(this.Register);
    }

    /// <summary>
    /// Gets the command to execute when the user submits the form.
    /// </summary>
    public ICommand RegisterCommand { get; }

    /// <summary>
    /// Gets or sets the error message in the form.
    /// </summary>
    public string ErrorMsg { get; set; }

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
    }

    /// <summary>
    /// Validates form inputs.
    /// </summary>
    private void ValidateInputs()
    {
        
    }
}
