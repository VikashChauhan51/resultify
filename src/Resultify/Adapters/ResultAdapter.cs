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

    /// <summary>
    /// The constructor takes a Result instance and initializes the adapter to represent its status and errors in a standardized format. The Status property reflects the status of the Result, and the Errors property provides access to any exception message if an exception is present; otherwise, it returns an empty dictionary.
    /// </summary>
    /// <param name="result"></param>
    public ResultAdapter(Result result) => _result = result;

    /// <summary>
    /// The Status property reflects the status of the underlying Result instance, allowing consumers to determine the result state (e.g., Success, Failure) based on the Result's status.
    /// </summary>
    public ResultState Status => _result.Status;

    /// <summary>
    /// The Errors property exposes the error information from the underlying Result instance as a dictionary. If the Result contains an exception, the dictionary includes a single entry with the key "Exception" and the value being the exception message. If there is no exception, the dictionary is empty.
    /// </summary>
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

    /// <summary>
    /// The constructor takes a Result<T> instance and initializes the adapter to represent its status, data, and errors in a standardized format. The Status property reflects the status of the Result<T>, the Data property provides access to the contained value (if any), and the Errors property exposes any exceptions in a structured dictionary format.
    /// </summary>
    /// <param name="result"></param>
    public ResultAdapter(Result<T> result) => _result = result;

    /// <summary>
    /// The Status property reflects the status of the underlying Result<T> instance, allowing consumers to determine the result state (e.g., Success, Failure) based on the Result<T>'s status.
    /// </summary>
    public ResultState Status => _result.Status;
    
    /// <summary>
    /// The Data property provides access to the value contained in the underlying Result<T> instance. If the Result<T> represents a successful result, this property returns the value; otherwise, it returns null.
    /// </summary>
    public T? Data => _result.Value;

    /// <summary>
    /// The Errors property exposes the error information from the underlying Result<T> instance as a dictionary. If the Result<T> contains an exception, the dictionary includes a single entry with the key "Exception" and the value being the exception message. If there is no exception, the dictionary is empty.
    /// </summary>
    public IReadOnlyDictionary<string, object> Errors =>
        _result.Exception == null
            ? new Dictionary<string, object>()
            : new Dictionary<string, object>
            {
                ["Exception"] = _result.Exception.Message
            };
}
