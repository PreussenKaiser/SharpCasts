namespace SharpCasts.Core.Validation.Inputs;

/// <summary>
/// The class that represents a text input.
/// </summary>
public class TextInput : Input
{
    /// <summary>
    /// The text input's value.
    /// </summary>
    private readonly string value;

    /// <summary>
    /// The maximum character count for the input's value.
    /// </summary>
    private readonly int maxLength;

    /// <summary>
    /// Initializes a new instance of the <see cref="TextInput"/> class.
    /// </summary>
    /// <param name="name">The input's label.</param>
    /// <param name="value">The text input's value.</param>
    /// <param name="maxLength">The maximum character count for the input's value.</param>
    public TextInput(string name, string value, int maxLength)
        : base(name)
    {
        this.value = value;
        this.maxLength = maxLength;
    }

    /// <summary>
    /// Determines if the text input meets specified constraints.
    /// </summary>
    /// <returns>An error message if invalid, empty string if valid.</returns>
    public override string ValidateInput()
        => string.IsNullOrWhiteSpace(this.value)
            ? this.FormatError("Please enter a value")
            : this.value.Length > this.maxLength
            ? this.FormatError($"Character length must be below {this.maxLength}")
            : string.Empty;
}
