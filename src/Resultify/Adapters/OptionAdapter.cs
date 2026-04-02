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

    public OptionAdapter(Option<T> option)
    {
        _option = option;
    }

    public ResultState Status =>
        Option.IsSome(_option) ? ResultState.Success : ResultState.NoContent;

    public T? Data => _option.Value;

    public IReadOnlyDictionary<string, object> Errors =>
        Option.IsSome(_option)
            ? new Dictionary<string, object>()
            : new Dictionary<string, object>
            {
                ["Option.None"] = "No value present in Option."
            };
}
