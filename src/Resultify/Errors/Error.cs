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
    /// <summary>
    /// The Success property represents a successful operation, with a default description indicating that the operation completed successfully. This can be used to indicate a successful result without needing to create a new instance of the Error record for success cases.
    /// </summary>
    public static readonly Error Success = new(ErrorType.Success, "Operation completed successfully.");

    /// <summary>
    /// The Failure method creates an Error instance representing a failure, with a default description that can be overridden by providing a custom description. This allows for consistent error handling while still providing flexibility in describing the specific failure that occurred.
    /// </summary>
    /// <param name="description"></param>
    /// <returns></returns>
    public static Error Failure(string description = "A failure occurred.") =>
        new(ErrorType.Failure, description);

    /// <summary>
    /// The Validation method creates an Error instance representing a validation error, with a default description that can be overridden by providing a custom description. This is useful for indicating issues related to input validation or other checks that failed during processing.
    /// </summary>
    /// <param name="description">A custom description for the validation error.</param>
    /// <returns>An Error instance representing a validation error.</returns>
    public static Error Validation(string description = "Validation error.") =>
        new(ErrorType.Validation, description);

    /// <summary>
    /// The NotFound method creates an Error instance representing a "not found" error, with a default description that can be overridden by providing a custom description. This is typically used to indicate that a requested resource could not be found, such as when querying a database or accessing an API endpoint that does not exist.
    /// </summary>
    /// <param name="description">A custom description for the "not found" error.</param>
    /// <returns>An Error instance representing a "not found" error.</returns>
    public static Error NotFound(string description = "Resource not found.") =>
        new(ErrorType.NotFound, description);
    
    /// <summary>
    /// The Conflict method creates an Error instance representing a conflict error, with a default description that can be overridden by providing a custom description. This is typically used to indicate that a request could not be completed due to a conflict with the current state of the resource.
    /// </summary>
    /// <param name="description">A custom description for the conflict error.</param>
    /// <returns>An Error instance representing a conflict error.</returns>
    public static Error Conflict(string description = "Conflict occurred.") =>
        new(ErrorType.Conflict, description);

    /// <summary>
    /// The Unauthorized method creates an Error instance representing an unauthorized access error, with a default description that can be overridden by providing a custom description. This is typically used to indicate that the user or client does not have the necessary permissions to access a resource or perform an action.
    /// </summary>
    /// <param name="description">A custom description for the unauthorized access error.</param>
    /// <returns>An Error instance representing an unauthorized access error.</returns>
    public static Error Unauthorized(string description = "Unauthorized access.") =>
        new(ErrorType.Unauthorized, description);

    /// <summary>
    /// The Unexpected method creates an Error instance representing an unexpected error, with a default description that can be overridden by providing a custom description. This is typically used to indicate that an error occurred that was not anticipated or does not fit into any of the other predefined error categories.
    /// </summary>
    /// <param name="description">A custom description for the unexpected error.</param>
    /// <returns>An Error instance representing an unexpected error.</returns>
    public static Error Unexpected(string description = "An unexpected error occurred.") =>
        new(ErrorType.Unexpected, description);

    /// <summary>
    /// The BadRequest method creates an Error instance representing a bad request error, with a default description that can be overridden by providing a custom description. This is typically used to indicate that the request made by the client was invalid or could not be processed due to issues such as malformed syntax, invalid parameters, or other client-side errors.
    /// </summary>
    /// <param name="description">A custom description for the bad request error.</param>
    /// <returns>An Error instance representing a bad request error.</returns>
    public static Error BadRequest(string description = "Bad request.") =>
      new(ErrorType.BadRequest, description);
    
    /// <summary>
    /// The UnprocessableEntity method creates an Error instance representing an unprocessable entity error, with a default description that can be overridden by providing a custom description. This is typically used to indicate that the server understands the content type of the request entity, but was unable to process the contained instructions.
    /// </summary>
    /// <param name="description">A custom description for the unprocessable entity error.</param>
    /// <returns>An Error instance representing an unprocessable entity error.</returns>
    public static Error UnprocessableEntity(string description = "Unprocessable entity.") =>
        new(ErrorType.UnprocessableEntity, description);

    /// <summary>
    /// The NotAcceptable method creates an Error instance representing a "not acceptable" error, with a default description that can be overridden by providing a custom description. This is typically used to indicate that the server cannot produce a response matching the list of acceptable values defined in the request's headers, such as when the client requests a specific content type that the server cannot provide.
    /// </summary>
    /// <param name="description">A custom description for the "not acceptable" error.</param>
    /// <returns>An Error instance representing a "not acceptable" error.</returns>
    public static Error NotAcceptable(string description = "Not acceptable.") =>
        new(ErrorType.NotAcceptable, description);
    
    /// <summary>
    /// The InternalServerError method creates an Error instance representing an internal server error, with a default description that can be overridden by providing a custom description. This is typically used to indicate that the server encountered an unexpected condition that prevented it from fulfilling the request.
    /// </summary>
    /// <param name="description">A custom description for the internal server error.</param>
    /// <returns>An Error instance representing an internal server error.</returns>
    public static Error InternalServerError(string description = "Internal server error.") =>
        new(ErrorType.InternalServerError, description);

}
