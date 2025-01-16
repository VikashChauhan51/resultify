namespace ResultifyCore;

/// <summary>
/// Represents a value that can be one of seven possible types.
/// </summary>
/// <typeparam name="T1">The first possible type.</typeparam>
/// <typeparam name="T2">The second possible type.</typeparam>
/// <typeparam name="T3">The third possible type.</typeparam>
/// <typeparam name="T4">The fourth possible type.</typeparam>
/// <typeparam name="T5">The fifth possible type.</typeparam>
/// <typeparam name="T6">The sixth possible type.</typeparam>
/// <typeparam name="T7">The seventh possible type.</typeparam>
public readonly struct OneOf<T1, T2, T3, T4, T5, T6, T7> : IEquatable<OneOf<T1, T2, T3, T4, T5, T6, T7>>, IComparable
{
    private readonly T1? _value1;
    private readonly T2? _value2;
    private readonly T3? _value3;
    private readonly T4? _value4;
    private readonly T5? _value5;
    private readonly T6? _value6;
    private readonly T7? _value7;
    private readonly OneOfType _type;

    /// <summary>
    /// Represents the type of value stored in the OneOf struct.
    /// </summary>
    public enum OneOfType
    {
        T1,
        T2,
        T3,
        T4,
        T5,
        T6,
        T7
    }

    public OneOf(T1 value) { _value1 = value; _value2 = default; _value3 = default; _value4 = default; _value5 = default; _value6 = default; _value7 = default; _type = OneOfType.T1; }
    public OneOf(T2 value) { _value1 = default; _value2 = value; _value3 = default; _value4 = default; _value5 = default; _value6 = default; _value7 = default; _type = OneOfType.T2; }
    public OneOf(T3 value) { _value1 = default; _value2 = default; _value3 = value; _value4 = default; _value5 = default; _value6 = default; _value7 = default; _type = OneOfType.T3; }
    public OneOf(T4 value) { _value1 = default; _value2 = default; _value3 = default; _value4 = value; _value5 = default; _value6 = default; _value7 = default; _type = OneOfType.T4; }
    public OneOf(T5 value) { _value1 = default; _value2 = default; _value3 = default; _value4 = default; _value5 = value; _value6 = default; _value7 = default; _type = OneOfType.T5; }
    public OneOf(T6 value) { _value1 = default; _value2 = default; _value3 = default; _value4 = default; _value5 = default; _value6 = value; _value7 = default; _type = OneOfType.T6; }
    public OneOf(T7 value) { _value1 = default; _value2 = default; _value3 = default; _value4 = default; _value5 = default; _value6 = default; _value7 = value; _type = OneOfType.T7; }

    public bool IsT1 => _type == OneOfType.T1;
    public bool IsT2 => _type == OneOfType.T2;
    public bool IsT3 => _type == OneOfType.T3;
    public bool IsT4 => _type == OneOfType.T4;
    public bool IsT5 => _type == OneOfType.T5;
    public bool IsT6 => _type == OneOfType.T6;
    public bool IsT7 => _type == OneOfType.T7;

    public T1 AsT1 => IsT1 ? _value1! : throw new InvalidOperationException("Not a T1 value.");
    public T2 AsT2 => IsT2 ? _value2! : throw new InvalidOperationException("Not a T2 value.");
    public T3 AsT3 => IsT3 ? _value3! : throw new InvalidOperationException("Not a T3 value.");
    public T4 AsT4 => IsT4 ? _value4! : throw new InvalidOperationException("Not a T4 value.");
    public T5 AsT5 => IsT5 ? _value5! : throw new InvalidOperationException("Not a T5 value.");
    public T6 AsT6 => IsT6 ? _value6! : throw new InvalidOperationException("Not a T6 value.");
    public T7 AsT7 => IsT7 ? _value7! : throw new InvalidOperationException("Not a T7 value.");

    public override bool Equals(object? obj) => obj is OneOf<T1, T2, T3, T4, T5, T6, T7> other && Equals(other);

    public bool Equals(OneOf<T1, T2, T3, T4, T5, T6, T7> other)
    {
        return _type == other._type && _type switch
        {
            OneOfType.T1 => EqualityComparer<T1>.Default.Equals(_value1, other._value1),
            OneOfType.T2 => EqualityComparer<T2>.Default.Equals(_value2, other._value2),
            OneOfType.T3 => EqualityComparer<T3>.Default.Equals(_value3, other._value3),
            OneOfType.T4 => EqualityComparer<T4>.Default.Equals(_value4, other._value4),
            OneOfType.T5 => EqualityComparer<T5>.Default.Equals(_value5, other._value5),
            OneOfType.T6 => EqualityComparer<T6>.Default.Equals(_value6, other._value6),
            OneOfType.T7 => EqualityComparer<T7>.Default.Equals(_value7, other._value7),
            _ => false
        };
    }

    public override int GetHashCode()
    {
        return _type switch
        {
            OneOfType.T1 => HashCode.Combine(_type, _value1),
            OneOfType.T2 => HashCode.Combine(_type, _value2),
            OneOfType.T3 => HashCode.Combine(_type, _value3),
            OneOfType.T4 => HashCode.Combine(_type, _value4),
            OneOfType.T5 => HashCode.Combine(_type, _value5),
            OneOfType.T6 => HashCode.Combine(_type, _value6),
            OneOfType.T7 => HashCode.Combine(_type, _value7),
            _ => 0
        };
    }

    public int CompareTo(object? obj)
    {
        if (obj is not OneOf<T1, T2, T3, T4, T5, T6, T7> other)
            throw new ArgumentException("Object is not a valid OneOf instance.");

        if (_type != other._type)
            return _type.CompareTo(other._type);

        return _type switch
        {
            OneOfType.T1 => Comparer<T1>.Default.Compare(_value1, other._value1),
            OneOfType.T2 => Comparer<T2>.Default.Compare(_value2, other._value2),
            OneOfType.T3 => Comparer<T3>.Default.Compare(_value3, other._value3),
            OneOfType.T4 => Comparer<T4>.Default.Compare(_value4, other._value4),
            OneOfType.T5 => Comparer<T5>.Default.Compare(_value5, other._value5),
            OneOfType.T6 => Comparer<T6>.Default.Compare(_value6, other._value6),
            OneOfType.T7 => Comparer<T7>.Default.Compare(_value7, other._value7),
            _ => throw new InvalidOperationException("Invalid comparison state.")
        };
    }


    /// <summary>
    /// Executes a function depending on the type of the current value.
    /// </summary>
    /// <param name="matchT1">The function to execute if the value is of type T1.</param>
    /// <param name="matchT2">The function to execute if the value is of type T2.</param>
    /// <param name="matchT3">The function to execute if the value is of type T3.</param>
    /// <param name="matchT4">The function to execute if the value is of type T4.</param>
    /// <param name="matchT5">The function to execute if the value is of type T5.</param>
    /// <param name="matchT6">The function to execute if the value is of type T6.</param>
    /// <param name="matchT7">The function to execute if the value is of type T7.</param>
    /// <returns>The result of the executed function.</returns>
    public TResult Match<TResult>(
        Func<T1, TResult> matchT1,
        Func<T2, TResult> matchT2,
        Func<T3, TResult> matchT3,
        Func<T4, TResult> matchT4,
        Func<T5, TResult> matchT5,
        Func<T6, TResult> matchT6,
        Func<T7, TResult> matchT7)
    {
        return _type switch
        {
            OneOfType.T1 => matchT1(_value1!),
            OneOfType.T2 => matchT2(_value2!),
            OneOfType.T3 => matchT3(_value3!),
            OneOfType.T4 => matchT4(_value4!),
            OneOfType.T5 => matchT5(_value5!),
            OneOfType.T6 => matchT6(_value6!),
            OneOfType.T7 => matchT7(_value7!),
            _ => throw new InvalidOperationException("Unknown type.")
        };
    }


    public static bool operator ==(OneOf<T1, T2, T3, T4, T5, T6, T7> left, OneOf<T1, T2, T3, T4, T5, T6, T7> right) => left.Equals(right);
    public static bool operator !=(OneOf<T1, T2, T3, T4, T5, T6, T7> left, OneOf<T1, T2, T3, T4, T5, T6, T7> right) => !left.Equals(right);
    public static bool operator <(OneOf<T1, T2, T3, T4, T5, T6, T7> left, OneOf<T1, T2, T3, T4, T5, T6, T7> right) => left.CompareTo(right) < 0;
    public static bool operator <=(OneOf<T1, T2, T3, T4, T5, T6, T7> left, OneOf<T1, T2, T3, T4, T5, T6, T7> right) => left.CompareTo(right) <= 0;
    public static bool operator >(OneOf<T1, T2, T3, T4, T5, T6, T7> left, OneOf<T1, T2, T3, T4, T5, T6, T7> right) => left.CompareTo(right) > 0;
    public static bool operator >=(OneOf<T1, T2, T3, T4, T5, T6, T7> left, OneOf<T1, T2, T3, T4, T5, T6, T7> right) => left.CompareTo(right) >= 0;
}

