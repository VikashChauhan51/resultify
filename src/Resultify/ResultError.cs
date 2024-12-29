namespace ResultifyCore;
public readonly record struct ResultError : IEquatable<ResultError>
{
    public string Code { get; }
    public string Message { get; }

    public ResultError(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public bool Equals(ResultError other)
    {
        return Code == other.Code && Message == other.Message;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Code, Message);
    }

    public override string ToString() => $"{Code}: {Message}";
}


