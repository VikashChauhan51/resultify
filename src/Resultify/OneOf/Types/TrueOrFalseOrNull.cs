namespace ResultifyCore.Types;

/// <summary>
/// Represents a value that can be True, False, or Null.
/// </summary>
public readonly struct TrueOrFalseOrNull
{
    private readonly OneOf<True, False, Null> _value;

    /// <summary>
    /// Represents the "True" type.
    /// </summary>
    public struct True { }

    /// <summary>
    /// Represents the "False" type.
    /// </summary>
    public struct False { }

    /// <summary>
    /// Represents the "Null" type.
    /// </summary>
    public struct Null { }

    /// <summary>
    /// Initializes a new instance of the <see cref="TrueOrFalseOrNull"/> struct with a "True" value.
    /// </summary>
    /// <param name="value">The "True" value.</param>
    public TrueOrFalseOrNull(True value)
    {
        _value = new OneOf<True, False, Null>(value);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TrueOrFalseOrNull"/> struct with a "False" value.
    /// </summary>
    /// <param name="value">The "False" value.</param>
    public TrueOrFalseOrNull(False value)
    {
        _value = new OneOf<True, False, Null>(value);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TrueOrFalseOrNull"/> struct with a "Null" value.
    /// </summary>
    /// <param name="value">The "Null" value.</param>
    public TrueOrFalseOrNull(Null value)
    {
        _value = new OneOf<True, False, Null>(value);
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
    /// Gets a value indicating whether the current instance represents "Null."
    /// </summary>
    public bool IsNull => _value.IsT3;

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

    /// <summary>
    /// Gets the value as "Null."
    /// </summary>
    /// <exception cref="OneOfException">Thrown when the current instance does not hold a "Null" value.</exception>
    public Null AsNull => _value.AsT3;

    /// <inheritdoc />
    public override string ToString() => IsTrue ? "True" : IsFalse ? "False" : "Null";

    /// <inheritdoc />
    public override bool Equals(object? obj) => obj is TrueOrFalseOrNull other && _value.Equals(other._value);

    /// <inheritdoc />
    public override int GetHashCode() => _value.GetHashCode();

    /// <summary>
    /// Implicitly converts a "True" value to a <see cref="TrueOrFalseOrNull"/> instance.
    /// </summary>
    /// <param name="value">The "True" value.</param>
    public static implicit operator TrueOrFalseOrNull(True value) => new(value);

    /// <summary>
    /// Implicitly converts a "False" value to a <see cref="TrueOrFalseOrNull"/> instance.
    /// </summary>
    /// <param name="value">The "False" value.</param>
    public static implicit operator TrueOrFalseOrNull(False value) => new(value);

    /// <summary>
    /// Implicitly converts a "Null" value to a <see cref="TrueOrFalseOrNull"/> instance.
    /// </summary>
    /// <param name="value">The "Null" value.</param>
    public static implicit operator TrueOrFalseOrNull(Null value) => new(value);

    public static bool operator ==(TrueOrFalseOrNull left, TrueOrFalseOrNull right) => left.Equals(right);

    public static bool operator !=(TrueOrFalseOrNull left, TrueOrFalseOrNull right) => !(left == right);
}
