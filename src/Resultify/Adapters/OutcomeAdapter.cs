namespace ResultifyCore.Adapters;

/// <summary>
/// Provides an adapter that exposes the status and errors of an Outcome instance in a standardized result format.
/// </summary>
/// <remarks>The OutcomeAdapter implements the IResult interface, enabling integration with APIs or systems that
/// expect a result abstraction. Errors without a specified code are assigned a unique identifier to ensure all errors
/// can be referenced distinctly.</remarks>
public sealed class OutcomeAdapter : IResult
{
    private readonly Outcome _outcome;

    /// <summary>
    /// The constructor takes an Outcome instance and initializes the adapter to represent its status and errors in a standardized format.
    /// </summary>
    /// <param name="outcome">The Outcome instance to be adapted.</param>
    public OutcomeAdapter(Outcome outcome) => _outcome = outcome;

    /// <summary>
    /// The Status property reflects the status of the underlying Outcome instance, allowing consumers to determine the result state (e.g., Success, Failure) based on the Outcome's status.
    /// </summary>
    public ResultState Status => _outcome.Status;

    /// <summary>
    /// The Errors property exposes the errors from the underlying Outcome instance as a dictionary. Each error is represented with a unique code as the key and the error message as the value. If an error does not have a specified code, a new GUID is generated to ensure uniqueness in the dictionary keys.
    /// </summary>
    public IReadOnlyDictionary<string, object> Errors =>
        _outcome.Errors.ToDictionary(
            e => string.IsNullOrEmpty(e.Code) ? Guid.NewGuid().ToString() : e.Code,
            e => (object)e.Message
        );
}

/// <summary>
/// Provides an adapter that exposes the status, data, and errors of an Outcome<T> instance in a standardized format
/// compatible with the IResult<T> interface.
/// </summary>
/// <remarks>Use this class to integrate Outcome<T> results with components that expect an IResult<T>
/// implementation. Errors from the underlying Outcome<T> are exposed as a dictionary with unique error codes as keys
/// and error messages as values.</remarks>
/// <typeparam name="T">The type of the data contained in the Outcome<T> instance.</typeparam>
public sealed class OutcomeAdapter<T> : IResult<T>
{
    private readonly Outcome<T> _outcome;

    /// <summary>
    /// The constructor takes an Outcome<T> instance and initializes the adapter to represent its status, data, and errors in a standardized format. The Status property reflects the status of the Outcome<T>, the Data property provides access to the contained value (if any), and the Errors property exposes any errors in a structured dictionary format.
    /// </summary>
    /// <param name="outcome"></param>
    public OutcomeAdapter(Outcome<T> outcome) => _outcome = outcome;

    /// <summary>
    /// The Status property reflects the status of the underlying Outcome<T> instance, allowing consumers to determine the result state (e.g., Success, Failure) based on the Outcome<T>'s status.
    /// </summary>
    public ResultState Status => _outcome.Status;
    
    /// <summary>
    /// The Data property provides access to the value contained in the underlying Outcome<T> instance. If the Outcome<T> represents a successful result, this property returns the value; otherwise, it returns null.
    /// </summary>
    public T? Data => _outcome.Value;

    /// <summary>
    /// The Errors property exposes the errors from the underlying Outcome<T> instance as a dictionary. Each error is represented with a unique code as the key and the error message as the value. If an error does not have a specified code, a new GUID is generated to ensure uniqueness in the dictionary keys.
    /// </summary>
    public IReadOnlyDictionary<string, object> Errors =>
        _outcome.Errors.ToDictionary(
            e => string.IsNullOrEmpty(e.Code) ? Guid.NewGuid().ToString() : e.Code,
            e => (object)e.Message
        );

}
