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

    public OutcomeAdapter(Outcome outcome) => _outcome = outcome;

    public ResultState Status => _outcome.Status;

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

    public OutcomeAdapter(Outcome<T> outcome) => _outcome = outcome;

    public ResultState Status => _outcome.Status;

    public T? Data => _outcome.Value;

    public IReadOnlyDictionary<string, object> Errors =>
        _outcome.Errors.ToDictionary(
            e => string.IsNullOrEmpty(e.Code) ? Guid.NewGuid().ToString() : e.Code,
            e => (object)e.Message
        );

}
