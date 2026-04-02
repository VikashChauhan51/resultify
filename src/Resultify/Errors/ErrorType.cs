namespace ResultifyCore.Errors;

/// <summary>
/// Error type enumeration representing various error conditions that can occur during an operation.
/// </summary>
public enum ErrorType
{
    /// <summary>
    /// The operation completed successfully.
    /// </summary>
    Success = 1,

    /// <summary>
    /// The resource was successfully created.
    /// </summary>
    Created = 2,

    /// <summary>
    /// The operation completed successfully but there is no content to return.
    /// </summary>
    NoContent = 3,

    /// <summary>
    /// The requested resource could not be found.
    /// </summary>
    NotFound = 4,

    /// <summary>
    /// The operation failed due to validation errors.
    /// </summary>
    Validation = 5,

    /// <summary>
    /// The operation encountered a problem or issue.
    /// </summary>
    Problem = 6,

    /// <summary>
    /// The operation could not be completed due to a conflict.
    /// </summary>
    Conflict = 7,

    /// <summary>
    /// Access to the resource is forbidden.
    /// </summary>
    Forbidden = 8,

    /// <summary>
    /// Authentication is required and has failed or has not yet been provided.
    /// </summary>
    Unauthorized = 9,

    /// <summary>
    /// The operation failed due to an unspecified error.
    /// </summary>
    Failure = 10,

    /// <summary>
    /// The operation failed due to a critical error.
    /// </summary>
    CriticalError = 11,

    /// <summary>
    /// The resource is currently unavailable.
    /// </summary>
    Unavailable = 12,

    /// <summary>
    /// Represents an unexpected condition that may occur during operation.
    /// </summary>
    Unexpected = 13,

    /// <summary>
    /// The request was malformed or invalid.
    /// </summary>
    BadRequest = 14,

    /// <summary>
    /// The request could not be processed due to semantic errors.
    /// </summary>
    UnprocessableEntity = 15,

    /// <summary>
    /// The requested content type or representation is not acceptable.
    /// </summary>
    NotAcceptable = 16,

    /// <summary>
    /// A generic internal server error occurred.
    /// </summary>
    InternalServerError = 17
}
