namespace SharpCasts.Core.Validation.Inputs;

/// <summary>
/// The class that represents a base input.
/// </summary>
public abstract class Input
{
    /// <summary>
    /// The name of the input, typically the same as it's label.
    /// </summary>
    private readonly string name;

    /// <summary>
    /// Initializes a new instance of the abstract <see cref="Input"/> class.
    /// </summary>
    /// <param name="name">The name of the input, typically the same as it's label.</param>
    protected Input(string name)
        => this.name = name;

    /// <summary>
    /// Determines if the input is valid.
    /// </summary>
    /// <returns>An error message if invalid, empty if valid.</returns>
    public abstract string ValidateInput();

    /// <summary>
    /// Formats the error for the input.
    /// </summary>
    /// <param name="msg">The error message to format.</param>
    /// <returns>
    /// The formatted error
    /// <br/>
    /// Example: "Username: Please enter a username"
    /// </returns>
    protected string FormatError(string msg)
        => $"{this.name}: {msg}";
}
