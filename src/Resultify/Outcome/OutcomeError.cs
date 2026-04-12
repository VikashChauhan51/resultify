using System.Text.Json;

namespace ResultifyCore;

/// <summary>
/// Represents a standardized error object used to describe issues that occur during the execution of an operation.
/// </summary>
/// <remarks>
/// The <see cref="OutcomeError"/> struct encapsulates detailed information about an error, including:
/// <list type="bullet">
/// <item><description>A machine-readable error <see cref="Code"/>.</description></item>
/// <item><description>A human-readable <see cref="Message"/> describing the error.</description></item>
/// <item><description>A <see cref="Severity"/> level indicating the importance of the error.</description></item>
/// <item><description>An optional <see cref="Identifier"/> used to associate the error with a specific field or context.</description></item>
/// </list>
/// <para>
/// This struct implements <see cref="IEquatable{T}"/> to allow value-based equality comparison.
/// Equality is determined using the <see cref="Code"/> and <see cref="Message"/> properties only.
/// </para>
/// <para>
/// The <see cref="ToString"/> method returns a JSON representation of the error.
/// </para>
/// </remarks>
/// <example>
/// <code>
/// var error1 = new OutcomeError("Invalid input");
///
/// var error2 = new OutcomeError("VAL001", "Input is required");
///
/// var error3 = new OutcomeError(
///     identifier: "Email",
///     code: "VAL002",
///     message: "Email format is invalid",
///     severity: ValidationSeverity.Warning);
///
/// Console.WriteLine(error3);
/// </code>
/// </example>
[Serializable]
public readonly record struct OutcomeError : IEquatable<OutcomeError>
{
    /// <summary>
    /// Gets the machine-readable error code.
    /// </summary>
    /// <remarks>
    /// This value is typically used for programmatic handling of errors.
    /// </remarks>
    public string Code { get; } = string.Empty;

    /// <summary>
    /// Gets the human-readable error message.
    /// </summary>
    /// <remarks>
    /// This message is intended to describe the error in a way that is understandable to developers or end users.
    /// </remarks>
    public string Message { get; } = string.Empty;

    /// <summary>
    /// Gets the severity level of the error.
    /// </summary>
    /// <remarks>
    /// Indicates the importance or impact of the error, such as informational, warning, or critical error.
    /// </remarks>
    public ValidationSeverity Severity { get; } = ValidationSeverity.Error;

    /// <summary>
    /// Gets the identifier associated with the error.
    /// </summary>
    /// <remarks>
    /// This can be used to associate the error with a specific field, property, or domain entity.
    /// For example, in validation scenarios, this may represent the field name.
    /// </remarks>
    public string Identifier { get; } = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="OutcomeError"/> struct with default values.
    /// </summary>
    /// <remarks>
    /// All properties are initialized to their default values.
    /// </remarks>
    public OutcomeError()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OutcomeError"/> struct with the specified message.
    /// </summary>
    /// <param name="message">The human-readable error message.</param>
    /// <remarks>
    /// The <see cref="Code"/> and <see cref="Identifier"/> will be empty,
    /// and <see cref="Severity"/> will default to <see cref="ValidationSeverity.Error"/>.
    /// </remarks>
    /// <example>
    /// <code>
    /// var error = new OutcomeError("Something went wrong.");
    /// </code>
    /// </example>
    public OutcomeError(string message)
    {
        Message = message;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OutcomeError"/> struct with the specified code and message.
    /// </summary>
    /// <param name="code">The machine-readable error code.</param>
    /// <param name="message">The human-readable error message.</param>
    /// <remarks>
    /// The <see cref="Identifier"/> will be empty,
    /// and <see cref="Severity"/> will default to <see cref="ValidationSeverity.Error"/>.
    /// </remarks>
    /// <example>
    /// <code>
    /// var error = new OutcomeError("ERR001", "Invalid request.");
    /// </code>
    /// </example>
    public OutcomeError(string code, string message)
    {
        Code = code;
        Message = message;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OutcomeError"/> struct with full error details.
    /// </summary>
    /// <param name="identifier">The identifier associated with the error (e.g., field name).</param>
    /// <param name="code">The machine-readable error code.</param>
    /// <param name="message">The human-readable error message.</param>
    /// <param name="severity">The severity level of the error.</param>
    /// <example>
    /// <code>
    /// var error = new OutcomeError(
    ///     identifier: "Password",
    ///     code: "VAL003",
    ///     message: "Password must be at least 8 characters.",
    ///     severity: ValidationSeverity.Error);
    /// </code>
    /// </example>
    public OutcomeError(string identifier, string code, string message, ValidationSeverity severity)
    {
        Identifier = identifier;
        Code = code;
        Message = message;
        Severity = severity;
    }

    /// <summary>
    /// Determines whether the specified <see cref="OutcomeError"/> is equal to the current instance.
    /// </summary>
    /// <param name="other">The error to compare with the current instance.</param>
    /// <returns>
    /// <c>true</c> if the <see cref="Code"/> and <see cref="Message"/> are equal; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// Equality comparison only considers <see cref="Code"/> and <see cref="Message"/>.
    /// Other properties such as <see cref="Identifier"/> and <see cref="Severity"/> are ignored.
    /// </remarks>
    public bool Equals(OutcomeError other)
    {
        return Code == other.Code && Message == other.Message;
    }

    /// <summary>
    /// Returns a hash code for the current instance.
    /// </summary>
    /// <returns>A hash code based on all properties of the error.</returns>
    /// <remarks>
    /// This implementation uses <see cref="HashCode.Combine(object?)"/> to generate a hash code
    /// from <see cref="Identifier"/>, <see cref="Code"/>, <see cref="Message"/>, and <see cref="Severity"/>.
    /// </remarks>
    public override int GetHashCode()
    {
        return HashCode.Combine(Identifier, Code, Message, Severity);
    }

    /// <summary>
    /// Returns a JSON string representation of the current error.
    /// </summary>
    /// <returns>A JSON-formatted string containing all properties of the error.</returns>
    /// <remarks>
    /// This method uses <see cref="JsonSerializer"/> to serialize the instance.
    /// </remarks>
    /// <example>
    /// <code>
    /// var error = new OutcomeError("ERR001", "Something failed");
    /// Console.WriteLine(error.ToString());
    /// </code>
    /// </example>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}


