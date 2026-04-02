namespace ResultifyCore.Errors;

/// <summary>
/// Represents an error with a specific type and description.
/// </summary>
/// <remarks>This record provides static methods to create common error instances, such as success, failure,
/// validation errors, and more. Each method allows for a custom description to be provided.</remarks>
/// <param name="Type">The type of the error, indicating the nature of the issue.</param>
/// <param name="Description">A description providing details about the error.</param>
public record Error(ErrorType Type, string Description)
{
    public static readonly Error Success = new(ErrorType.Success, "Operation completed successfully.");
    public static Error Failure(string description = "A failure occurred.") =>
        new(ErrorType.Failure, description);

    public static Error Validation(string description = "Validation error.") =>
        new(ErrorType.Validation, description);

    public static Error NotFound(string description = "Resource not found.") =>
        new(ErrorType.NotFound, description);

    public static Error Conflict(string description = "Conflict occurred.") =>
        new(ErrorType.Conflict, description);

    public static Error Unauthorized(string description = "Unauthorized access.") =>
        new(ErrorType.Unauthorized, description);

    public static Error Unexpected(string description = "An unexpected error occurred.") =>
        new(ErrorType.Unexpected, description);
    public static Error BadRequest(string description = "Bad request.") =>
      new(ErrorType.BadRequest, description);

    public static Error UnprocessableEntity(string description = "Unprocessable entity.") =>
        new(ErrorType.UnprocessableEntity, description);

    public static Error NotAcceptable(string description = "Not acceptable.") =>
        new(ErrorType.NotAcceptable, description);

    public static Error InternalServerError(string description = "Internal server error.") =>
        new(ErrorType.InternalServerError, description);

}
