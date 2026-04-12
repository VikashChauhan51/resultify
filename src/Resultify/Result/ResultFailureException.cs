namespace ResultifyCore;

/// <summary>
/// Exception thrown when an attempt is made to unwrap a failed Result.
/// </summary>
public class ResultFailureException : Exception
{
    /// <summary>
    /// The default constructor initializes a new instance of the <see cref="ResultFailureException"/> class with a default error message indicating that the result was a failure. This exception is typically thrown when code attempts to access the value of a Result that represents a failure, providing a clear indication of the error condition.
    /// </summary>
    public ResultFailureException()
        : base("Result was a failure")
    {
    }

    /// <summary>
    /// The constructor that takes a string message allows the caller to specify a custom error message when throwing the exception. This can provide more context about the failure, such as details about why the result was a failure or what specific error occurred. This constructor is useful for providing more informative error messages to developers or end users when handling exceptions related to failed Results.
    /// </summary>
    /// <param name="message">The custom error message that describes the failure.</param>
    public ResultFailureException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// The constructor that takes a string message and an inner exception allows the caller to specify both a custom error message and an underlying exception that caused the failure. This is useful for preserving the original exception details while providing additional context about the failure. When this constructor is used, the inner exception can be accessed through the InnerException property of the ResultFailureException, allowing developers to trace back to the root cause of the failure when handling exceptions related to failed Results.
    /// </summary>
    /// <param name="message">The custom error message that describes the failure.</param>
    /// <param name="innerException">The underlying exception that caused the failure.</param>
    public ResultFailureException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}

