namespace ResultifyCore;

/// <summary>
/// Represents a value that can be one of two possible types.
/// </summary>
/// <typeparam name="T1">The first possible type.</typeparam>
/// <typeparam name="T2">The second possible type.</typeparam>
public readonly struct OneOf<T1, T2> : IEquatable<OneOf<T1, T2>>, IComparable
{
    private readonly T1? _value1;
    private readonly T2? _value2;
    private readonly OneOfType _type;

    /// <summary>
    /// Represents the type of value stored in the OneOf struct.
    /// </summary>
    public enum OneOfType
    {
        /// <summary>
        /// Indicates that the value is of type T1.
        /// </summary>
        T1,

        /// <summary>
        /// Indicates that the value is of type T2.
        /// </summary>
        T2
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OneOf{T1, T2}"/> struct with a value of type T1.
    /// </summary>
    /// <param name="value">The value of type T1.</param>
    public OneOf(T1 value)
    {
        _value1 = value;
        _value2 = default;
        _type = OneOfType.T1;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OneOf{T1, T2}"/> struct with a value of type T2.
    /// </summary>
    /// <param name="value">The value of type T2.</param>
    public OneOf(T2 value)
    {
        _value1 = default;
        _value2 = value;
        _type = OneOfType.T2;
    }

    /// <summary>
    /// Gets a value indicating whether the current instance holds a value of type T1.
    /// </summary>
    public bool IsT1 => _type == OneOfType.T1;

    /// <summary>
    /// Gets a value indicating whether the current instance holds a value of type T2.
    /// </summary>
    public bool IsT2 => _type == OneOfType.T2;

    /// <summary>
    /// Gets the value of type T1.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when the current instance does not hold a value of type T1.</exception>
    public T1 AsT1 => IsT1 ? _value1! : throw new InvalidOperationException("Not a T1 value.");

    /// <summary>
    /// Gets the value of type T2.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when the current instance does not hold a value of type T2.</exception>
    public T2 AsT2 => IsT2 ? _value2! : throw new InvalidOperationException("Not a T2 value.");

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is OneOf<T1, T2> other && Equals(other);
    }

    /// <inheritdoc />
    public bool Equals(OneOf<T1, T2> other)
    {
        return _type == other._type &&
               (_type == OneOfType.T1
                   ? EqualityComparer<T1>.Default.Equals(_value1, other._value1)
                   : EqualityComparer<T2>.Default.Equals(_value2, other._value2));
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return _type switch
        {
            OneOfType.T1 => HashCode.Combine(_value1),
            OneOfType.T2 => HashCode.Combine(_value2),
            _ => 0
        };
    }

    /// <summary>
    /// Compares this instance with another object.
    /// </summary>
    /// <param name="obj">The object to compare.</param>
    /// <returns>
    /// A value less than zero if this instance precedes <paramref name="obj"/>; 
    /// zero if this instance is equal to <paramref name="obj"/>; 
    /// or a value greater than zero if this instance follows <paramref name="obj"/>.
    /// </returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="obj"/> is not a <see cref="OneOf{T1, T2}"/>.</exception>
    public int CompareTo(object? obj)
    {
        if (obj is OneOf<T1, T2> other)
        {
            if (_type != other._type)
            {
                return _type.CompareTo(other._type); // Compare by type first
            }

            return _type switch
            {
                OneOfType.T1 => Comparer<T1>.Default.Compare(_value1, other._value1),
                OneOfType.T2 => Comparer<T2>.Default.Compare(_value2, other._value2),
                _ => throw new InvalidOperationException("Invalid comparison state.")
            };
        }

        throw new ArgumentException("Object is not a valid OneOf instance.");
    }


    /// <summary>
    /// Executes a function depending on the type of the current value.
    /// </summary>
    /// <param name="matchT1">The function to execute if the value is of type T1.</param>
    /// <param name="matchT2">The function to execute if the value is of type T2.</param>
    /// <returns>The result of the executed function.</returns>
    public TResult Match<TResult>(Func<T1, TResult> matchT1, Func<T2, TResult> matchT2)
    {
        return _type switch
        {
            OneOfType.T1 => matchT1(_value1!),
            OneOfType.T2 => matchT2(_value2!),
            _ => throw new InvalidOperationException("Unknown type.")
        };
    }


    /// <inheritdoc />
    public static bool operator ==(OneOf<T1, T2> left, OneOf<T1, T2> right) => left.Equals(right);

    /// <inheritdoc />
    public static bool operator !=(OneOf<T1, T2> left, OneOf<T1, T2> right) => !left.Equals(right);

    /// <inheritdoc />
    public static bool operator <(OneOf<T1, T2> left, OneOf<T1, T2> right) => left.CompareTo(right) < 0;

    /// <inheritdoc />
    public static bool operator <=(OneOf<T1, T2> left, OneOf<T1, T2> right) => left.CompareTo(right) <= 0;

    /// <inheritdoc />
    public static bool operator >(OneOf<T1, T2> left, OneOf<T1, T2> right) => left.CompareTo(right) > 0;

    /// <inheritdoc />
    public static bool operator >=(OneOf<T1, T2> left, OneOf<T1, T2> right) => left.CompareTo(right) >= 0;
}

