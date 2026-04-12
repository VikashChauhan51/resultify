namespace ResultifyCore;

/// <summary>
/// The <see cref="OutcomeExtensions"/> class provides extension methods for creating and working with <see cref="Outcome"/> and <see cref="Outcome{T}"/> instances. These methods offer a convenient way to create success and failure outcomes, allowing developers to easily construct outcomes based on their needs. The extension methods include functionality for creating failure outcomes with specified errors and success outcomes with a given value, streamlining the process of working with outcomes in a consistent and readable manner.
/// </summary>
public static class OutcomeExtensions
{

    /// <summary>
    /// Creates a failure outcome with the provided errors.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="errors">The errors describing the failure.</param>
    /// <returns>A failed <see cref="Outcome{T}"/>.</returns>
    public static Outcome<T> Failure<T>(this IEnumerable<OutcomeError> errors) => Outcome<T>.Failure(errors);

    /// <summary>
    /// Creates a failure outcome with the provided errors.
    /// </summary>
    /// <param name="errors">The errors describing the failure.</param>
    /// <returns>A failed <see cref="Outcome"/>.</returns>
    public static Outcome Failure(this IEnumerable<OutcomeError> errors) => Outcome.Failure(errors);

    /// <summary>
    /// Creates a successful outcome with the provided value.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="value">The value to include in the outcome.</param>
    /// <returns>A successful <see cref="Outcome{T}"/> with the value.</returns>
    public static Outcome<T> SuccessOutcome<T>(this T value) => Outcome<T>.Success(value);
}
