namespace ResultifyCore.Exceptions;

/// <summary>
/// Exception thrown when an invalid OneOf type operation is performed.
/// </summary>
public class OneOfException : Exception
{
    public OneOfException()
        : base("Invalid OneOf type.")
    {
    }

    public OneOfException(string message)
        : base(message)
    {
    }

    public OneOfException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}

