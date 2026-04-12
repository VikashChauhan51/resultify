namespace ResultifyCore.Exceptions;

/// <summary>
/// Base exception for all Resultify exceptions.
/// Provides HTTP status code and structured error details.
/// </summary>
/// <remarks>
/// This exception standardizes error handling across the application.
/// It allows APIs to return consistent error responses.
/// </remarks>
/// <example>
/// <code>
/// throw new ResultifyBaseException("Something went wrong");
/// </code>
/// </example>
public class ResultifyBaseException : Exception
{
    /// <summary>
    /// Gets the HTTP status code associated with this exception.
    /// </summary>
    public int StatusCode { get; }

    /// <summary>
    /// Gets the structured error details.
    /// </summary>
    public IReadOnlyDictionary<string, object> Errors { get; }

    /// <summary>
    /// Initializes a new instance with message and optional status code.
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <param name="statusCode">HTTP status code (default 500).</param>
    public ResultifyBaseException(string message, int statusCode = 500)
        : base(message)
    {
        StatusCode = statusCode;
        Errors = new Dictionary<string, object> { [""] = message };
    }

    /// <summary>
    /// Initializes a new instance with message, inner exception, and status code.
    /// </summary>
    public ResultifyBaseException(string message, Exception innerException, int statusCode = 500)
        : base(message, innerException)
    {
        StatusCode = statusCode;
        Errors = new Dictionary<string, object> { [""] = message };
    }

    /// <summary>
    /// Initializes a new instance with message and structured errors.
    /// </summary>
    public ResultifyBaseException(string message, IReadOnlyDictionary<string, object> errors, int statusCode = 500)
        : base(message)
    {
        StatusCode = statusCode;
        Errors = errors;
    }
}

/// <summary>
/// Represents HTTP 404 Not Found error.
/// </summary>
/// <example>
/// <code>
/// throw new ResultifyNotFoundException("User not found");
/// </code>
/// </example>
public class ResultifyNotFoundException : ResultifyBaseException
{
    /// <summary>Initializes with message.</summary>
    public ResultifyNotFoundException(string message) : base(message, 404) { }

    /// <summary>Initializes with message and inner exception.</summary>
    public ResultifyNotFoundException(string message, Exception innerException)
        : base(message, innerException, 404) { }

    /// <summary>Initializes with message and structured errors.</summary>
    public ResultifyNotFoundException(string message, IReadOnlyDictionary<string, object> errors)
        : base(message, errors, 404) { }
}

/// <summary>
/// Represents HTTP 400 Bad Request error.
/// </summary>
/// <example>
/// <code>
/// throw new ResultifyBadRequestException("Invalid input");
/// </code>
/// </example>
public class ResultifyBadRequestException : ResultifyBaseException
{
    /// <summary>
    /// The constructor initializes a new instance of the ResultifyBadRequestException class with an optional message parameter. If no message is provided, it defaults to "Bad request." The exception is associated with the HTTP status code 400, indicating that the client's request was invalid or cannot be processed by the server. This exception can be thrown in scenarios where the client provides incorrect data, fails validation, or makes a request that cannot be fulfilled due to client-side issues.
    /// </summary>
    /// <param name="message">The error message.</param>
    public ResultifyBadRequestException(string message = "Bad request.") : base(message, 400) { }

    /// <summary>
    /// The constructor initializes a new instance of the ResultifyBadRequestException class with a specified error message and an inner exception. The inner exception provides additional context about the error that occurred, allowing for more detailed error handling and debugging. This constructor is useful when you want to capture the original exception that led to the bad request error while still providing a custom message for the client. The exception is associated with the HTTP status code 400, indicating that the client's request was invalid or cannot be processed by the server.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="innerException">The inner exception that caused the current exception.</param>
    public ResultifyBadRequestException(string message, Exception innerException)
        : base(message, innerException, 400) { }

    /// <summary>
    /// The constructor initializes a new instance of the ResultifyBadRequestException class with a specified error message and a dictionary of structured errors. The structured errors provide detailed information about the specific issues that led to the bad request, allowing for more granular error reporting and handling. This constructor is useful when you want to convey multiple validation errors or other client-side issues in a structured format. The exception is associated with the HTTP status code 400, indicating that the client's request was invalid or cannot be processed by the server.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="errors"></param>
    public ResultifyBadRequestException(string message, IReadOnlyDictionary<string, object> errors)
        : base(message, errors, 400) { }
}

/// <summary>
/// Represents HTTP 401 Unauthorized error.
/// </summary>
/// <example>
/// <code>
/// throw new ResultifyUnauthorizedException();
/// </code>
/// </example>
public class ResultifyUnauthorizedException : ResultifyBaseException
{
    /// <summary>
    /// The constructor initializes a new instance of the ResultifyUnauthorizedException class with an optional message parameter. If no message is provided, it defaults to "Unauthorized." The exception is associated with the HTTP status code 401, indicating that the client must authenticate itself to get the requested response. This exception can be thrown in scenarios where the client fails to provide valid authentication credentials or does not have permission to access the requested resource.
    /// </summary>
    /// <param name="message">The error message.</param>
    public ResultifyUnauthorizedException(string message = "Unauthorized.") : base(message, 401) { }

    /// <summary>
    /// The constructor initializes a new instance of the ResultifyUnauthorizedException class with a specified error message and an inner exception. The inner exception provides additional context about the error that occurred, allowing for more detailed error handling and debugging. This constructor is useful when you want to capture the original exception that led to the unauthorized access error while still providing a custom message for the client. The exception is associated with the HTTP status code 401, indicating that the client must authenticate itself to get the requested response.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="innerException">The inner exception that caused the current exception.</param>
    public ResultifyUnauthorizedException(string message, Exception innerException)
        : base(message, innerException, 401) { }
    
    /// <summary>
    /// The constructor initializes a new instance of the ResultifyUnauthorizedException class with a specified error message and a dictionary of structured errors. The structured errors provide detailed information about the specific issues that led to the unauthorized access, allowing for more granular error reporting and handling. This constructor is useful when you want to convey multiple validation errors or other client-side issues in a structured format. The exception is associated with the HTTP status code 401, indicating that the client must authenticate itself to get the requested response.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="errors">A dictionary of structured errors.</param>
    public ResultifyUnauthorizedException(string message, IReadOnlyDictionary<string, object> errors)
        : base(message, errors, 401) { }
}

/// <summary>
/// Represents HTTP 403 Forbidden error.
/// </summary>
/// <example>
/// <code>
/// throw new ResultifyForbiddenException("Access denied");
/// </code>
/// </example>
public class ResultifyForbiddenException : ResultifyBaseException
{
    /// <summary>
    /// The constructor initializes a new instance of the ResultifyForbiddenException class with an optional message parameter. If no message is provided, it defaults to "Forbidden." The exception is associated with the HTTP status code 403, indicating that the server understands the request but refuses to authorize it. This exception can be thrown in scenarios where the client does not have permission to access the requested resource, even if they are authenticated.
    /// </summary>
    /// <param name="message">The error message.</param>
    public ResultifyForbiddenException(string message = "Forbidden.") : base(message, 403) { }

    /// <summary>
    /// The constructor initializes a new instance of the ResultifyForbiddenException class with a specified error message and an inner exception. The inner exception provides additional context about the error that occurred, allowing for more detailed error handling and debugging. This constructor is useful when you want to capture the original exception that led to the forbidden access error while still providing a custom message for the client. The exception is associated with the HTTP status code 403, indicating that the server understands the request but refuses to authorize it.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="innerException">The inner exception that caused the current exception.</param>
    public ResultifyForbiddenException(string message, Exception innerException)
        : base(message, innerException, 403) { }

    /// <summary>
    /// The constructor initializes a new instance of the ResultifyForbiddenException class with a specified error message and a dictionary of structured errors. The structured errors provide detailed information about the specific issues that led to the forbidden access, allowing for more granular error reporting and handling. This constructor is useful when you want to convey multiple validation errors or other client-side issues in a structured format. The exception is associated with the HTTP status code 403, indicating that the server understands the request but refuses to authorize it.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="errors">A dictionary of structured errors.</param>
    /// <param name="statusCode">The HTTP status code.</param>
    public ResultifyForbiddenException(string message, IReadOnlyDictionary<string, object> errors, int statusCode = 403)
        : base(message, errors, statusCode) { }
}

/// <summary>
/// Represents HTTP 409 Conflict error.
/// </summary>
/// <example>
/// <code>
/// throw new ResultifyConflictException("Duplicate record");
/// </code>
/// </example>
public class ResultifyConflictException : ResultifyBaseException
{
    /// <summary>
    /// The constructor initializes a new instance of the ResultifyConflictException class with an optional message parameter. If no message is provided, it defaults to "Conflict." The exception is associated with the HTTP status code 409, indicating that the request could not be completed due to a conflict with the current state of the target resource. This exception can be thrown in scenarios where there is a conflict in the data, such as attempting to create a duplicate record or update a resource that has been modified by another process.    
    /// </summary>
    /// <param name="message">The error mes</param>
    public ResultifyConflictException(string message = "Conflict.") : base(message, 409) { }

    /// <summary>
    /// The constructor initializes a new instance of the ResultifyConflictException class with a specified error message and an inner exception. The inner exception provides additional context about the error that occurred, allowing for more detailed error handling and debugging. This constructor is useful when you want to capture the original exception that led to the conflict error while still providing a custom message for the client. The exception is associated with the HTTP status code 409, indicating that the request could not be completed due to a conflict with the current state of the target resource.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="innerException">The inner exception.</param>
    public ResultifyConflictException(string message, Exception innerException)
        : base(message, innerException, 409) { }

    /// <summary>
    /// The constructor initializes a new instance of the ResultifyConflictException class with a specified error message and a dictionary of structured errors. The structured errors provide detailed information about the specific issues that led to the conflict, allowing for more granular error reporting and handling. This constructor is useful when you want to convey multiple validation errors or other client-side issues in a structured format. The exception is associated with the HTTP status code 409, indicating that the request could not be completed due to a conflict with the current state of the target resource.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="errors">The dictionary of structured errors.</param>
    public ResultifyConflictException(string message, IReadOnlyDictionary<string, object> errors)
        : base(message, errors, 409) { }
}

/// <summary>
/// Represents HTTP 422 Unprocessable Entity error.
/// </summary>
/// <example>
/// <code>
/// throw new ResultifyUnprocessableEntityException("Validation failed");
/// </code>
/// </example>
public class ResultifyUnprocessableEntityException : ResultifyBaseException
{
    /// <summary>
    /// The constructor initializes a new instance of the ResultifyUnprocessableEntityException class with an optional message parameter. If no message is provided, it defaults to "Unprocessable entity." The exception is associated with the HTTP status code 422, indicating that the server understands the content type of the request entity and the syntax of the request entity is correct, but it was unable to process the contained instructions. This exception can be thrown in scenarios where the request is well-formed but contains semantic errors or fails validation rules, such as when required fields are missing or when data formats are incorrect.
    /// </summary>
    /// <param name="message">The error message.</param>
    public ResultifyUnprocessableEntityException(string message = "Unprocessable entity.")
        : base(message, 422) { }

    /// <summary>
    /// The constructor initializes a new instance of the ResultifyUnprocessableEntityException class with a specified error message and an inner exception. The inner exception provides additional context about the error that occurred, allowing for more detailed error handling and debugging. This constructor is useful when you want to capture the original exception that led to the unprocessable entity error while still providing a custom message for the client. The exception is associated with the HTTP status code 422, indicating that the server understands the content type of the request entity and the syntax of the request entity is correct, but it was unable to process the contained instructions.    
    /// </summary>
    /// <param name="message"></param>
    /// <param name="innerException"></param>
    public ResultifyUnprocessableEntityException(string message, Exception innerException)
        : base(message, innerException, 422) { }

    /// <summary>
    /// The constructor initializes a new instance of the ResultifyUnprocessableEntityException class with a specified error message and a dictionary of structured errors. The structured errors provide detailed information about the specific issues that led to the unprocessable entity error, allowing for more granular error reporting and handling. This constructor is useful when you want to convey multiple validation errors or other client-side issues in a structured format. The exception is associated with the HTTP status code 422, indicating that the server understands the content type of the request entity and the syntax of the request entity is correct, but it was unable to process the contained instructions.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="errors">The dictionary of structured errors.</param>
    public ResultifyUnprocessableEntityException(string message, IReadOnlyDictionary<string, object> errors)
        : base(message, errors, 422) { }
}

/// <summary>
/// Represents HTTP 429 Too Many Requests error.
/// </summary>
/// <example>
/// <code>
/// throw new ResultifyTooManyRequestsException();
/// </code>
/// </example>
public class ResultifyTooManyRequestsException : ResultifyBaseException
{
    /// <summary>
    /// The constructor initializes a new instance of the ResultifyTooManyRequestsException class with an optional message parameter. If no message is provided, it defaults to "Too many requests." The exception is associated with the HTTP status code 429, indicating that the user has sent too many requests in a given amount of time ("rate limiting"). This exception can be thrown in scenarios where the client exceeds the allowed number of requests within a specified time frame, such as when implementing API rate limiting to prevent abuse or overuse of resources.
    /// </summary>
    /// <param name="message">The error message.</param>
    public ResultifyTooManyRequestsException(string message = "Too many requests.")
        : base(message, 429) { }

    /// <summary>
    /// The constructor initializes a new instance of the ResultifyTooManyRequestsException class with a specified error message and an inner exception. The inner exception provides additional context about the error that occurred, allowing for more detailed error handling and debugging. This constructor is useful when you want to capture the original exception that led to the too many requests error while still providing a custom message for the client. The exception is associated with the HTTP status code 429, indicating that the user has sent too many requests in a given amount of time ("rate limiting").
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="innerException">The inner exception.</param>
    public ResultifyTooManyRequestsException(string message, Exception innerException)
        : base(message, innerException, 429) { }

    /// <summary>
    /// The constructor initializes a new instance of the ResultifyTooManyRequestsException class with a specified error message and a dictionary of structured errors. The structured errors provide detailed information about the specific issues that led to the too many requests error, allowing for more granular error reporting and handling. This constructor is useful when you want to convey multiple validation errors or other client-side issues in a structured format. The exception is associated with the HTTP status code 429, indicating that the user has sent too many requests in a given amount of time ("rate limiting").
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="errors">The dictionary of structured errors.</param>
    public ResultifyTooManyRequestsException(string message, IReadOnlyDictionary<string, object> errors)
        : base(message, errors, 429) { }
}

/// <summary>
/// Represents HTTP 503 Service Unavailable error.
/// </summary>
/// <example>
/// <code>
/// throw new ResultifyUnavailableException("Service down");
/// </code>
/// </example>
public class ResultifyUnavailableException : ResultifyBaseException
{
    /// <summary>
    /// The constructor initializes a new instance of the ResultifyUnavailableException class with an optional message parameter. If no message is provided, it defaults to "Service unavailable." The exception is associated with the HTTP status code 503, indicating that the server is currently unable to handle the request due to temporary overloading or maintenance of the server. This exception can be thrown in scenarios where the service is temporarily unavailable, such as during server maintenance or when the server is experiencing high traffic and cannot process additional requests.
    /// </summary>
    /// <param name="message">The error message.</param>
    public ResultifyUnavailableException(string message) : base(message, 503) { }

    /// <summary>
    /// The constructor initializes a new instance of the ResultifyUnavailableException class with a specified error message and an inner exception. The inner exception provides additional context about the error that occurred, allowing for more detailed error handling and debugging. This constructor is useful when you want to capture the original exception that led to the service unavailable error while still providing a custom message for the client. The exception is associated with the HTTP status code 503, indicating that the server is currently unable to handle the request due to temporary overloading or maintenance of the server.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="innerException">The inner exception.</param>
    public ResultifyUnavailableException(string message, Exception innerException)
        : base(message, innerException, 503) { }

    /// <summary>
    /// The constructor initializes a new instance of the ResultifyUnavailableException class with a specified error message and a dictionary of structured errors. The structured errors provide detailed information about the specific issues that led to the service unavailable error, allowing for more granular error reporting and handling. This constructor is useful when you want to convey multiple validation errors or other client-side issues in a structured format. The exception is associated with the HTTP status code 503, indicating that the server is currently unable to handle the request due to temporary overloading or maintenance of the server.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="errors">A dictionary of structured errors.</param>
    /// <param name="statusCode">The HTTP status code.</param>
    public ResultifyUnavailableException(string message, IReadOnlyDictionary<string, object> errors, int statusCode = 503)
        : base(message, errors, statusCode) { }
}

/// <summary>
/// Represents validation errors (HTTP 400).
/// </summary>
/// <example>
/// <code>
/// throw new ResultifyValidationException("Validation failed", errors);
/// </code>
/// </example>
public class ResultifyValidationException : ResultifyBaseException
{
    /// <summary>
    /// The constructor initializes a new instance of the ResultifyValidationException class with an optional message parameter. If no message is provided, it defaults to "Validation failed." The exception is associated with the HTTP status code 400, indicating that the client's request was invalid or cannot be processed by the server due to validation errors. This exception can be thrown in scenarios where the client provides incorrect data or fails to meet validation rules, allowing for detailed error reporting and handling of validation issues.
    /// </summary>
    /// <param name="message">The error message.</param>
    public ResultifyValidationException(string message = "Validation failed.")
        : base(message, 400) { }

    /// <summary>
    /// The constructor initializes a new instance of the ResultifyValidationException class with a specified error message and an inner exception. The inner exception provides additional context about the error that occurred, allowing for more detailed error handling and debugging. This constructor is useful when you want to capture the original exception that led to the validation error while still providing a custom message for the client. The exception is associated with the HTTP status code 400, indicating that the client's request was invalid or cannot be processed by the server due to validation errors.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="innerException">The inner exception that caused the current exception.</param>
    public ResultifyValidationException(string message, Exception innerException)
        : base(message, innerException, 400) { }

    /// <summary>
    /// The constructor initializes a new instance of the ResultifyValidationException class with a specified error message and a dictionary of structured errors. The structured errors provide detailed information about the specific issues that led to the validation error, allowing for more granular error reporting and handling. This constructor is useful when you want to convey multiple validation errors or other client-side issues in a structured format. The exception is associated with the HTTP status code 400, indicating that the client's request was invalid or cannot be processed by the server due to validation errors.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="errors">A dictionary of structured errors.</param>
    /// <param name="statusCode">The HTTP status code.</param>
    public ResultifyValidationException(string message, IReadOnlyDictionary<string, object> errors, int statusCode = 400)
        : base(message, errors, statusCode) { }
}

/// <summary>
/// Represents unexpected system errors (HTTP 500).
/// </summary>
/// <example>
/// <code>
/// throw new ResultifyUnexpectedException();
/// </code>
/// </example>
public class ResultifyUnexpectedException : ResultifyBaseException
{
    /// <summary>
    /// The constructor initializes a new instance of the ResultifyUnexpectedException class with an optional message parameter. If no message is provided, it defaults to "An unexpected error occurred." The exception is associated with the HTTP status code 500, indicating that an unexpected condition was encountered on the server and the request could not be fulfilled. This exception can be thrown in scenarios where an unhandled exception occurs or when there is a critical failure in the application that prevents it from processing the request successfully. It serves as a catch-all for unforeseen errors that may arise during the execution of the application.
    /// </summary>
    /// <param name="message">The error message.</param>
    public ResultifyUnexpectedException(string message = "An unexpected error occurred.")
        : base(message, 500) { }

    /// <summary>
    /// The constructor initializes a new instance of the ResultifyUnexpectedException class with a specified error message and an inner exception. The inner exception provides additional context about the error that occurred, allowing for more detailed error handling and debugging. This constructor is useful when you want to capture the original exception that led to the unexpected error while still providing a custom message for the client. The exception is associated with the HTTP status code 500, indicating that an unexpected condition was encountered on the server and the request could not be fulfilled.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="innerException">The inner exception that caused the current exception. </param>
    public ResultifyUnexpectedException(string message, Exception innerException)
        : base(message, innerException, 500) { }

    /// <summary>
    /// The constructor initializes a new instance of the ResultifyUnexpectedException class with a specified error message and a dictionary of structured errors. The structured errors provide detailed information about the specific issues that led to the unexpected error, allowing for more granular error reporting and handling. This constructor is useful when you want to convey multiple errors or other server-side issues in a structured format. The exception is associated with the HTTP status code 500, indicating that an unexpected condition was encountered on the server and the request could not be fulfilled.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="errors">A dictionary of structured errors.</param>
    /// <param name="statusCode">The HTTP status code.</param>
    public ResultifyUnexpectedException(string message, IReadOnlyDictionary<string, object> errors, int statusCode = 500)
        : base(message, errors, statusCode) { }
}
