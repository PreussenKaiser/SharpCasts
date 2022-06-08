using SharpCasts.Core.Validation.Inputs;

namespace SharpCasts.Core.Validation;

/// <summary>
/// The class which builds a list of inputs and validates them.
/// </summary>
public sealed class Validator
{
    /// <summary>
    /// The inputs to validate.
    /// </summary>
    private readonly List<Input> inputs;

    /// <summary>
    /// Initializes a new instance of the <see cref="Validator"/> class.
    /// </summary>
    public Validator()
        => this.inputs = new List<Input>();

    /// <summary>
    /// Clears all inputs from the validator.
    /// </summary>
    public Validator Reset()
    {
        this.inputs.Clear();

        return this;
    }

    /// <summary>
    /// Adds an input to the validator.
    /// </summary>
    /// <param name="input">The input to add.</param>
    /// <returns></returns>
    public Validator AddInput(Input input)
    {
        this.inputs.Add(input);

        return this;
    }

    /// <summary>
    /// Validates the form.
    /// </summary>
    /// <returns>Any error messages as a result of validation.</returns>
    public string Validate()
    {
        foreach (Input input in this.inputs)
        {
            string msg = input.ValidateInput();

            if (!string.IsNullOrEmpty(msg))
                return msg;
        }

        return string.Empty;
    }
}
