namespace ResultifyCore;

/// <summary>
/// Represents the severity level of a validation result.
/// </summary>
public enum ValidationSeverity : byte
{
    /// <summary>
    /// Indicates a validation error.
    /// </summary>
    Error = 1,

    /// <summary>
    /// Indicates a validation warning.
    /// </summary>
    Warning = 2,

    /// <summary>
    /// Indicates informational validation.
    /// </summary>
    Info = 3
}
