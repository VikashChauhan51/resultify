namespace ResultifyCore.Result;

/// <summary>
/// Exception thrown when an attempt is made to unwrap a failed Result.
/// </summary>
public class ResultFailureException : Exception
{
    public ResultFailureException()
        : base("Result was a failure")
    {
    }

    public ResultFailureException(string message)
        : base(message)
    {
    }

    public ResultFailureException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}

