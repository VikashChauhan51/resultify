using ResultifyCore.Exceptions;

namespace ResultifyCore;

/// <summary>
/// Represents a value that can be one of two possible types.
/// </summary>
/// <typeparam name="T1">The first possible type.</typeparam>
/// <typeparam name="T2">The second possible type.</typeparam>
public readonly struct OneOf<T1, T2>
{
    private readonly T1 _value1;
    private readonly T2 _value2;
    private readonly OneOfType _type;

    /// <summary>
    /// Initializes a new instance of the <see cref="OneOf{T1, T2}"/> struct with a value of type T1.
    /// </summary>
    /// <param name="value">The value of type T1.</param>
    public OneOf(T1 value)
    {
        _value1 = value;
        _value2 = default!;
        _type = OneOfType.T1;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OneOf{T1, T2}"/> struct with a value of type T2.
    /// </summary>
    /// <param name="value">The value of type T2.</param>
    public OneOf(T2 value)
    {
        _value1 = default!;
        _value2 = value;
        _type = OneOfType.T2;
    }

    /// <summary>
    /// Creates a new instance of the <see cref="OneOf{T1, T2}"/> struct with a value of type T1.
    /// </summary>
    /// <param name="value">The value of type T1.</param>
    /// <returns>A new <see cref="OneOf{T1, T2}"/> instance.</returns>
    public static OneOf<T1, T2> FromT1(T1 value) => new OneOf<T1, T2>(value);

    /// <summary>
    /// Creates a new instance of the <see cref="OneOf{T1, T2}"/> struct with a value of type T2.
    /// </summary>
    /// <param name="value">The value of type T2.</param>
    /// <returns>A new <see cref="OneOf{T1, T2}"/> instance.</returns>
    public static OneOf<T1, T2> FromT2(T2 value) => new OneOf<T1, T2>(value);

    /// <summary>
    /// Matches the value stored in the <see cref="OneOf{T1, T2}"/> instance with the corresponding function.
    /// </summary>
    /// <typeparam name="TResult">The return type of the match functions.</typeparam>
    /// <param name="f1">The function to handle the value of type T1.</param>
    /// <param name="f2">The function to handle the value of type T2.</param>
    /// <returns>The result of the matched function.</returns>
    /// <exception cref="OneOfException">Thrown when the <see cref="OneOf{T1, T2}"/> instance contains an invalid type.</exception>
    public TResult Match<TResult>(Func<T1, TResult> f1, Func<T2, TResult> f2)
    {
        return _type switch
        {
            OneOfType.T1 => f1(_value1),
            OneOfType.T2 => f2(_value2),
            _ => throw new OneOfException()
        };
    }

    /// <summary>
    /// Matches the value stored in the <see cref="OneOf{T1, T2}"/> instance with the corresponding action.
    /// </summary>
    /// <param name="f1">The action to handle the value of type T1.</param>
    /// <param name="f2">The action to handle the value of type T2.</param>
    /// <exception cref="OneOfException">Thrown when the <see cref="OneOf{T1, T2}"/> instance contains an invalid type.</exception>
    public void Match(Action<T1> f1, Action<T2> f2)
    {
        switch (_type)
        {
            case OneOfType.T1:
                f1(_value1);
                break;
            case OneOfType.T2:
                f2(_value2);
                break;
            default:
                throw new OneOfException();
        }
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
    /// <exception cref="OneOfException">Thrown when the current instance does not hold a value of type T1.</exception>
    public T1 AsT1 => IsT1 ? _value1 : throw new OneOfException("Not a T1 value.");

    /// <summary>
    /// Gets the value of type T2.
    /// </summary>
    /// <exception cref="OneOfException">Thrown when the current instance does not hold a value of type T2.</exception>
    public T2 AsT2 => IsT2 ? _value2 : throw new OneOfException("Not a T2 value.");
}
