using SharpCasts.Core.Models;
using SharpCasts.Core.Services;

namespace SharpCasts.Core.Validation.Inputs;

/// <summary>
/// Represents validation for registering users.
/// </summary>
public class RegisterInput : Input
{
    /// <summary>
    /// The username to search for.
    /// </summary>
    private readonly string username;

    /// <summary>
    /// The service to validate the registering user with.
    /// </summary>
    private readonly IUserService userService;

    /// <summary>
    /// The input's label.
    /// </summary>
    private const string LABEL = "Credentials";

    /// <summary>
    /// Initializes a new instance of the <see cref="RegisterInput"/> class.
    /// </summary>
    /// <param name="username">The username to search for.</param>
    /// <param name="userService">The service to validate the registering user with.</param>
    public RegisterInput(string username, IUserService userService)
        : base(LABEL)
    {
        this.username = username;
        this.userService = userService;
    }

    /// <summary>
    /// Determines if a user with the same
    /// </summary>
    /// <returns></returns>
    public override string ValidateInput()
    {
        User user = this.userService.GetUserByUsername(this.username);

        return user is null
            ? string.Empty
            : this.FormatError("A user with those credentials already exists.");
    }
}
