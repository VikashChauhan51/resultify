namespace ResultifyCore;

/// <summary>
/// Exception thrown when an attempt is made to unwrap an Option that has no value.
/// </summary>
public class OptionNoneException : Exception
{
    public OptionNoneException()
        : base("Option was none")
    {
    }

    public OptionNoneException(string message)
        : base(message)
    {
    }

    public OptionNoneException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
