namespace ResultifyCore;

public static class ResultExtensions
{
    /// <summary>
    /// Executes an action regardless of whether the result is successful or failed.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="result">The result to apply the action on.</param>
    /// <param name="action">The action to execute.</param>
    /// <returns>The original result to allow method chaining.</returns>
    public static Result<T> Do<T>(this Result<T> result, Action<Result<T>> action)
    {
        action(result);
        return result;
    }

    /// <summary>
    /// Transforms the value inside a successful result.
    /// </summary>
    /// <typeparam name="T">The type of the original value.</typeparam>
    /// <typeparam name="U">The type of the transformed value.</typeparam>
    /// <param name="result">The result to transform.</param>
    /// <param name="map">The transformation function.</param>
    /// <returns>A new result with the transformed value if successful, otherwise the original failure result.</returns>
    public static Result<U> Map<T, U>(this Result<T> result, Func<T, U> map)
    {
        return result.Status == ResultState.Success ? Result<U>.Success(map(result.Value!)) : Result<U>.Failure(result.Status, result.Exception!);
    }

    /// <summary>
    /// Chains multiple operations that return results.
    /// </summary>
    /// <typeparam name="T">The type of the original value.</typeparam>
    /// <typeparam name="U">The type of the value of the next result.</typeparam>
    /// <param name="result">The result to chain from.</param>
    /// <param name="bind">The function that returns the next result.</param>
    /// <returns>The result of the next operation if the current result is successful, otherwise the original failure result.</returns>
    public static Result<U> Bind<T, U>(this Result<T> result, Func<T, Result<U>> bind)
    {
        return result.Status == ResultState.Success ? bind(result.Value!) : Result<U>.Failure(result.Status, result.Exception!);
    }

    /// <summary>
    /// Executes a side-effect action without altering the result.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="result">The result to tap into.</param>
    /// <param name="tapAction">The action to execute.</param>
    /// <returns>The original result to allow method chaining.</returns>
    public static Result<T> Tap<T>(this Result<T> result, Action<T> tapAction)
    {
        if (result.Status == ResultState.Success)
        {
            tapAction(result.Value!);
        }
        return result;
    }

    /// <summary>
    /// Creates a successful result containing the specified value.
    /// </summary>
    /// <typeparam name="T">The type of the value contained in the result.</typeparam>
    /// <param name="value">The value to be encapsulated in the successful result.</param>
    /// <returns>A <see cref="Result{T}"/> containing the specified value.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the specified value is an <see cref="Exception"/>. 
    /// This method only accepts non-<see cref="Exception"/> types for success results.</exception>
    public static Result<T> Success<T>(this T value)
    {
        if (value is Exception)
            throw new InvalidOperationException("Cannot use an Exception as a value.");
        return new Result<T>(value);
    }

    /// <summary>
    /// Creates a failed result containing the specified exception.
    /// </summary>
    /// <typeparam name="T">The type of the value contained in the result. This is not used in failure results.</typeparam>
    /// <param name="exception">The exception to be encapsulated in the failure result.</param>
    /// <returns>A <see cref="Result{T}"/> representing a failure with the specified exception.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the specified exception is null. 
    /// Failure results must be initialized with a valid exception.</exception>
    public static Result<T> Failure<T>(this Exception exception)
    {
        if (exception == null)
            throw new ArgumentNullException(nameof(exception), "Exception cannot be null.");
        return new Result<T>(ResultState.Failure, exception);
    }

    public static Result<T> Conflict<T>(this Exception exception)
    {
        if (exception == null)
            throw new ArgumentNullException(nameof(exception), "Exception cannot be null.");
        return new Result<T>(ResultState.Conflict, exception);
    }

    public static Result<T> Problem<T>(this Exception exception)
    {
        if (exception == null)
            throw new ArgumentNullException(nameof(exception), "Exception cannot be null.");
        return new Result<T>(ResultState.Problem, exception);
    }

    public static Result<T> Validation<T>(this Exception exception)
    {
        if (exception == null)
            throw new ArgumentNullException(nameof(exception), "Exception cannot be null.");
        return new Result<T>(ResultState.Validation, exception);
    }

    public static Result<T> NotFound<T>(this Exception exception)
    {
        if (exception == null)
            throw new ArgumentNullException(nameof(exception), "Exception cannot be null.");
        return new Result<T>(ResultState.NotFound, exception);
    }

    public static Result<T> Unauthorized<T>(this Exception exception)
    {
        if (exception == null)
            throw new ArgumentNullException(nameof(exception), "Exception cannot be null.");
        return new Result<T>(ResultState.Unauthorized, exception);
    }

    /// <summary>
    /// Creates a failed result containing the specified exception.
    /// </summary>
    /// <param name="exception">The exception to be encapsulated in the failure result.</param>
    /// <returns>A <see cref="Result"/> representing a failure with the specified exception.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the specified exception is null. 
    /// Failure results must be initialized with a valid exception.</exception>
    public static Result Failure(this Exception exception)
    {
        if (exception == null)
            throw new ArgumentNullException(nameof(exception), "Exception cannot be null.");
        return Result.Failure(ResultState.Failure, exception);
    }

    public static Result Conflict(this Exception exception)
    {
        if (exception == null)
            throw new ArgumentNullException(nameof(exception), "Exception cannot be null.");
        return Result.Failure(ResultState.Conflict, exception);
    }

    public static Result Problem(this Exception exception)
    {
        if (exception == null)
            throw new ArgumentNullException(nameof(exception), "Exception cannot be null.");
        return Result.Failure(ResultState.Problem, exception);
    }

    public static Result Validation(this Exception exception)
    {
        if (exception == null)
            throw new ArgumentNullException(nameof(exception), "Exception cannot be null.");
        return Result.Failure(ResultState.Validation, exception);
    }

    public static Result NotFound(this Exception exception)
    {
        if (exception == null)
            throw new ArgumentNullException(nameof(exception), "Exception cannot be null.");
        return Result.Failure(ResultState.NotFound, exception);
    }

    public static Result Unauthorized(this Exception exception)
    {
        if (exception == null)
            throw new ArgumentNullException(nameof(exception), "Exception cannot be null.");
        return Result.Failure(ResultState.Unauthorized, exception);
    }

}


