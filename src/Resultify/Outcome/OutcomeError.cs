namespace ResultifyCore;
public readonly record struct OutcomeError : IEquatable<OutcomeError>
{
    public string Code { get; }
    public string Message { get; }

    public OutcomeError(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public bool Equals(OutcomeError other)
    {
        return Code == other.Code && Message == other.Message;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Code, Message);
    }

    public override string ToString() => $"{Code}: {Message}";
}


