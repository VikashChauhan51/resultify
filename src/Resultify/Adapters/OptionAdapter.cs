namespace ResultifyCore.Adapters;

/// <summary>
/// Provides an adapter that exposes an Option<T> as an IResult<T>, allowing Option values to be represented using the
/// result pattern.
/// </summary>
/// <remarks>Use this class to bridge Option<T> values with APIs or consumers that expect IResult<T>
/// implementations. The Status property indicates whether the Option contains a value, and the Errors property provides
/// information when no value is present.</remarks>
/// <typeparam name="T">The type of the value contained in the Option.</typeparam>
public sealed class OptionAdapter<T> : IResult<T>
{
    private readonly Option<T> _option;

    /// <summary>
    /// Takes an Option<T> and initializes the adapter to represent its state as an IResult<T>. If the Option contains a value,
    /// the adapter will represent a successful result. If the Option is empty, the adapter will represent a no-content result.
    /// </summary>
    /// <param name="option">The Option<T> to be adapted.</param>
    public OptionAdapter(Option<T> option)
    {
        _option = option;
    }

    /// <summary>
    /// The Status property indicates the result state based on the presence of a value in the Option.
    /// If the Option contains a value, it returns ResultState.Success.
    /// </summary>
    public ResultState Status =>
        Option.IsSome(_option) ? ResultState.Success : ResultState.NoContent;

    /// <summary>
    /// The Data property returns the value contained in the Option if it is present. If the Option is empty, it returns null.
    /// </summary>
    public T? Data => _option.Value;

    /// <summary>
    /// The Errors property provides a dictionary of error information when the Option is empty. If the Option contains a value, it returns an empty dictionary.
    /// </summary>
    public IReadOnlyDictionary<string, object> Errors =>
        Option.IsSome(_option)
            ? new Dictionary<string, object>()
            : new Dictionary<string, object>
            {
                ["Option.None"] = "No value present in Option."
            };
}
