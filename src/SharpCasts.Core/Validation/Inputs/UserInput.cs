using SharpCasts.Core.Models;
using SharpCasts.Core.Services;

namespace SharpCasts.Core.Validation.Inputs;

/// <summary>
/// The class which represents input from a user's credentials.
/// </summary>
public class UserInput : Input
{
    /// <summary>
    /// The user to validate.
    /// </summary>
    private readonly User user;

    /// <summary>
    /// The user service to check the user's existence with.
    /// </summary>
    private readonly IUserService userService;

    /// <summary>
    /// The input's label.
    /// </summary>
    private const string LABEL = "Credentials";

    /// <summary>
    /// Initializes a new instance of the <see cref="UserInput"/> class.
    /// </summary>
    /// <param name="user">The user to validate.</param>
    /// <param name="userService">The user service to check the user's existence with.</param>
    public UserInput(User user, IUserService userService)
        : base(LABEL)
    {
        this.user = user;
        this.userService = userService;
    }

    /// <summary>
    /// Determines if the user exists in the provided service.
    /// </summary>
    /// <returns>An error message if the user doesn't exist, empty string if they do.</returns>
    public override string ValidateInput()
        => this.userService.GetUserByCredentials(this.user) is null
            ? this.FormatError("Incorrect username or password")
            : string.Empty;
}
