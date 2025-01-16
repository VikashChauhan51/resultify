namespace ResultifyCore.Types;


/// <summary>
/// Represents a value that can be either True or False.
/// </summary>
public readonly struct TrueOrFalse
{
    private readonly OneOf<True, False> _value;

    /// <summary>
    /// Represents the "True" type.
    /// </summary>
    public struct True { }

    /// <summary>
    /// Represents the "False" type.
    /// </summary>
    public struct False { }

    /// <summary>
    /// Initializes a new instance of the <see cref="TrueOrFalse"/> struct with a "True" value.
    /// </summary>
    /// <param name="value">The "True" value.</param>
    public TrueOrFalse(True value)
    {
        _value = new OneOf<True, False>(value);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TrueOrFalse"/> struct with a "False" value.
    /// </summary>
    /// <param name="value">The "False" value.</param>
    public TrueOrFalse(False value)
    {
        _value = new OneOf<True, False>(value);
    }

    /// <summary>
    /// Gets a value indicating whether the current instance represents "True."
    /// </summary>
    public bool IsTrue => _value.IsT1;

    /// <summary>
    /// Gets a value indicating whether the current instance represents "False."
    /// </summary>
    public bool IsFalse => _value.IsT2;

    /// <summary>
    /// Gets the value as "True."
    /// </summary>
    /// <exception cref="OneOfException">Thrown when the current instance does not hold a "True" value.</exception>
    public True AsTrue => _value.AsT1;

    /// <summary>
    /// Gets the value as "False."
    /// </summary>
    /// <exception cref="OneOfException">Thrown when the current instance does not hold a "False" value.</exception>
    public False AsFalse => _value.AsT2;

    /// <inheritdoc />
    public override string ToString() => IsTrue ? "True" : "False";

    /// <inheritdoc />
    public override bool Equals(object? obj) => obj is TrueOrFalse other && _value.Equals(other._value);

    /// <inheritdoc />
    public override int GetHashCode() => _value.GetHashCode();

    /// <summary>
    /// Implicitly converts a "True" value to a <see cref="TrueOrFalse"/> instance.
    /// </summary>
    /// <param name="value">The "True" value.</param>
    public static implicit operator TrueOrFalse(True value) => new(value);

    /// <summary>
    /// Implicitly converts a "False" value to a <see cref="TrueOrFalse"/> instance.
    /// </summary>
    /// <param name="value">The "False" value.</param>
    public static implicit operator TrueOrFalse(False value) => new(value);

    public static bool operator ==(TrueOrFalse left, TrueOrFalse right) => left.Equals(right);

    public static bool operator !=(TrueOrFalse left, TrueOrFalse right) => !(left == right);
}

