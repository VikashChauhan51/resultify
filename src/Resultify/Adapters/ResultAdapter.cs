namespace ResultifyCore.Adapters;

/// <summary>
/// Provides a read-only adapter for the Result type, exposing its status and any associated errors in a structured
/// format.
/// </summary>
/// <remarks>Use this class to access the status and error information of a Result instance through the IResult
/// interface. If the underlying Result contains an exception, the exception message is included in the Errors
/// dictionary with the key "Exception". Otherwise, the Errors dictionary is empty.</remarks>
public sealed class ResultAdapter : IResult
{
    private readonly Result _result;

    public ResultAdapter(Result result) => _result = result;

    public ResultState Status => _result.Status;

    public IReadOnlyDictionary<string, object> Errors =>
        _result.Exception == null
            ? new Dictionary<string, object>()
            : new Dictionary<string, object>
            {
                ["Exception"] = _result.Exception.Message
            };
}


/// <summary>
/// Provides a standardized adapter that exposes the status, data, and errors of a wrapped result instance.
/// </summary>
/// <remarks>Use this class to interact with results from operations in a consistent manner, regardless of the
/// underlying result implementation. The adapter allows consumers to access the operation's status, retrieve the data
/// if available, and inspect any associated errors.</remarks>
/// <typeparam name="T">The type of the value contained in the result.</typeparam>
public sealed class ResultAdapter<T> : IResult<T>
{
    private readonly Result<T> _result;

    public ResultAdapter(Result<T> result) => _result = result;

    public ResultState Status => _result.Status;

    public T? Data => _result.Value;

    public IReadOnlyDictionary<string, object> Errors =>
        _result.Exception == null
            ? new Dictionary<string, object>()
            : new Dictionary<string, object>
            {
                ["Exception"] = _result.Exception.Message
            };
}
