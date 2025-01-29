using System.Text.Json;

namespace ResultifyCore;

[Serializable]
public readonly record struct OutcomeError : IEquatable<OutcomeError>
{
    public string Code { get; } = string.Empty;
    public string Message { get; } = string.Empty;
    public ValidationSeverity Severity { get; } = ValidationSeverity.Error;
    public string Identifier { get; } = string.Empty;

    public OutcomeError()
    {
    }

    public OutcomeError(string message)
    {
        Message = message;
    }

    public OutcomeError(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public OutcomeError(string identifier, string code, string message, ValidationSeverity severity)
    {
        Identifier = identifier;
        Code = code;
        Message = message;
        Severity = severity;
    }

    public bool Equals(OutcomeError other)
    {
        return Code == other.Code && Message == other.Message;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Identifier, Code, Message, Severity);
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}


