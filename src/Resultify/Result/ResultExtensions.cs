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
        return result.Status == ResultState.Success ? Result<U>.Success(map(result.Value!)) : Result<U>.WithError(result.Status, result.Exception!);
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
        return result.Status == ResultState.Success ? bind(result.Value!) : Result<U>.WithError(result.Status, result.Exception!);
    }

    /// <summary>
    /// Creates a result indicating a conflict with the specified exception.
    /// </summary>
    /// <param name="exception">The exception to be encapsulated in the conflict result.</param>
    /// <returns>A <see cref="Result"/> representing a conflict with the specified exception.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the specified exception is null. 
    /// Conflict results must be initialized with a valid exception.</exception>
    public static Result Conflict(this Exception exception)
    {
        Guard.Against.Null(exception, nameof(exception));
        return Result.Failure(ResultState.Conflict, exception);
    }

    /// <summary>
    /// Creates a result indicating a conflict with the specified exception.
    /// </summary>
    /// <typeparam name="T">The type of the value contained in the result. This is not used in conflict results.</typeparam>
    /// <param name="exception">The exception to be encapsulated in the conflict result.</param>
    /// <returns>A <see cref="Result{T}"/> representing a conflict with the specified exception.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the specified exception is null. 
    /// Conflict results must be initialized with a valid exception.</exception>
    public static Result<T> Conflict<T>(this Exception exception)
    {
        Guard.Against.Null(exception, nameof(exception));
        return new Result<T>(ResultState.Conflict, exception);
    }

    /// <summary>
    /// Creates a result indicating a problem with the specified exception.
    /// </summary>
    /// <param name="exception">The exception to be encapsulated in the problem result.</param>
    /// <returns>A <see cref="Result"/> representing a problem with the specified exception.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the specified exception is null. 
    /// Problem results must be initialized with a valid exception.</exception>
    public static Result Problem(this Exception exception)
    {
        Guard.Against.Null(exception, nameof(exception));
        return Result.Failure(ResultState.Problem, exception);
    }

    /// <summary>
    /// Creates a result indicating a problem with the specified exception.
    /// </summary>
    /// <typeparam name="T">The type of the value contained in the result. This is not used in problem results.</typeparam>
    /// <param name="exception">The exception to be encapsulated in the problem result.</param>
    /// <returns>A <see cref="Result{T}"/> representing a problem with the specified exception.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the specified exception is null. 
    /// Problem results must be initialized with a valid exception.</exception>
    public static Result<T> Problem<T>(this Exception exception)
    {
        Guard.Against.Null(exception, nameof(exception));
        return new Result<T>(ResultState.Problem, exception);
    }

    /// <summary>
    /// Creates a result indicating a validation error with the specified exception.
    /// </summary>
    /// <param name="exception">The exception to be encapsulated in the validation result.</param>
    /// <returns>A <see cref="Result"/> representing a validation error with the specified exception.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the specified exception is null. 
    /// Validation results must be initialized with a valid exception.</exception>
    public static Result Validation(this Exception exception)
    {
        Guard.Against.Null(exception, nameof(exception));
        return Result.Failure(ResultState.Validation, exception);
    }

    /// <summary>
    /// Creates a result indicating a validation error with the specified exception.
    /// </summary>
    /// <typeparam name="T">The type of the value contained in the result. This is not used in validation results.</typeparam>
    /// <param name="exception">The exception to be encapsulated in the validation result.</param>
    /// <returns>A <see cref="Result{T}"/> representing a validation error with the specified exception.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the specified exception is null. 
    /// Validation results must be initialized with a valid exception.</exception>
    public static Result<T> Validation<T>(this Exception exception)
    {
        Guard.Against.Null(exception, nameof(exception));
        return new Result<T>(ResultState.Validation, exception);
    }

    /// <summary>
    /// Creates a result indicating that the requested resource was not found with the specified exception.
    /// </summary>
    /// <param name="exception">The exception to be encapsulated in the not found result.</param>
    /// <returns>A <see cref="Result"/> representing a not found error with the specified exception.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the specified exception is null. 
    /// Not found results must be initialized with a valid exception.</exception>
    public static Result NotFound(this Exception exception) 
    {
        Guard.Against.Null(exception, nameof(exception));
        return Result.Failure(ResultState.NotFound, exception);
    }

    /// <summary>
    /// Creates a result indicating that the requested resource was not found with the specified exception.
    /// </summary>
    /// <typeparam name="T">The type of the value contained in the result. This is not used in not found results.</typeparam>
    /// <param name="exception">The exception to be encapsulated in the not found result.</param>
    /// <returns>A <see cref="Result{T}"/> representing a not found error with the specified exception.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the specified exception is null. 
    /// Not found results must be initialized with a valid exception.</exception>
    public static Result<T> NotFound<T>(this Exception exception)
    {
        Guard.Against.Null(exception, nameof(exception));
        return new Result<T>(ResultState.NotFound, exception);
    }

    /// <summary>
    /// Creates a result indicating that the operation is unauthorized with the specified exception.
    /// </summary>
    /// <param name="exception">The exception to be encapsulated in the unauthorized result.</param>
    /// <returns>A <see cref="Result"/> representing an unauthorized error with the specified exception.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the specified exception is null. 
    /// Unauthorized results must be initialized with a valid exception.</exception>
    public static Result Unauthorized(this Exception exception)
    {
        Guard.Against.Null(exception, nameof(exception));
        return Result.Failure(ResultState.Unauthorized, exception);
    }

    /// <summary>
    /// Creates a result indicating that the operation is unauthorized with the specified exception.
    /// </summary>
    /// <typeparam name="T">The type of the value contained in the result. This is not used in unauthorized results.</typeparam>
    /// <param name="exception">The exception to be encapsulated in the unauthorized result.</param>
    /// <returns>A <see cref="Result{T}"/> representing an unauthorized error with the specified exception.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the specified exception is null. 
    /// Unauthorized results must be initialized with a valid exception.</exception>
    public static Result<T> Unauthorized<T>(this Exception exception)
    {
        Guard.Against.Null(exception, nameof(exception));
        return new Result<T>(ResultState.Unauthorized, exception);
    }

    /// <summary>
    /// Creates a result indicating that access to the resource is forbidden with the specified exception.
    /// </summary>
    /// <param name="exception">The exception to be encapsulated in the forbidden result.</param>
    /// <returns>A <see cref="Result"/> representing a forbidden error with the specified exception.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the specified exception is null. 
    /// Forbidden results must be initialized with a valid exception.</exception>
    public static Result Forbidden(this Exception exception)
    {
        Guard.Against.Null(exception, nameof(exception));
        return Result.Failure(ResultState.Forbidden, exception);
    }

    /// <summary>
    /// Creates a result indicating that access to the resource is forbidden with the specified exception.
    /// </summary>
    /// <typeparam name="T">The type of the value contained in the result. This is not used in forbidden results.</typeparam>
    /// <param name="exception">The exception to be encapsulated in the forbidden result.</param>
    /// <returns>A <see cref="Result{T}"/> representing a forbidden error with the specified exception.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the specified exception is null. 
    /// Forbidden results must be initialized with a valid exception.</exception>
    public static Result<T> Forbidden<T>(this Exception exception)
    {
        Guard.Against.Null(exception, nameof(exception));
        return new Result<T>(ResultState.Forbidden, exception);
    }

    /// <summary>
    /// Creates a result indicating that the resource is currently unavailable with the specified exception.
    /// </summary>
    /// <param name="exception">The exception to be encapsulated in the unavailable result.</param>
    /// <returns>A <see cref="Result"/> representing an unavailable error with the specified exception.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the specified exception is null. 
    /// Unavailable results must be initialized with a valid exception.</exception>
    public static Result Unavailable(this Exception exception)
    {
        Guard.Against.Null(exception, nameof(exception));
        return Result.Failure(ResultState.Unavailable, exception);
    }

    /// <summary>
    /// Creates a result indicating that the resource is currently unavailable with the specified exception.
    /// </summary>
    /// <typeparam name="T">The type of the value contained in the result. This is not used in unavailable results.</typeparam>
    /// <param name="exception">The exception to be encapsulated in the unavailable result.</param>
    /// <returns>A <see cref="Result{T}"/> representing an unavailable error with the specified exception.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the specified exception is null. 
    /// Unavailable results must be initialized with a valid exception.</exception>
    public static Result<T> Unavailable<T>(this Exception exception)
    {
        Guard.Against.Null(exception, nameof(exception));
        return new Result<T>(ResultState.Unavailable, exception);
    }

    /// <summary>
    /// Creates a result indicating a critical error with the specified exception.
    /// </summary>
    /// <param name="exception">The exception to be encapsulated in the critical error result.</param>
    /// <returns>A <see cref="Result"/> representing a critical error with the specified exception.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the specified exception is null. 
    /// Critical error results must be initialized with a valid exception.</exception>
    public static Result CriticalError(this Exception exception)
    {
        Guard.Against.Null(exception, nameof(exception));
        return Result.Failure(ResultState.CriticalError, exception);
    }

    /// <summary>
    /// Creates a result indicating a critical error with the specified exception.
    /// </summary>
    /// <typeparam name="T">The type of the value contained in the result. This is not used in critical error results.</typeparam>
    /// <param name="exception">The exception to be encapsulated in the critical error result.</param>
    /// <returns>A <see cref="Result{T}"/> representing a critical error with the specified exception.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the specified exception is null. 
    /// Critical error results must be initialized with a valid exception.</exception>
    public static Result<T> CriticalError<T>(this Exception exception)
    {
        Guard.Against.Null(exception, nameof(exception));
        return new Result<T>(ResultState.CriticalError, exception);
    }

    /// <summary>
    /// Determines whether the specified result state indicates success.
    /// </summary>
    /// <param name="status">The result state to check.</param>
    /// <returns><c>true</c> if the specified result state indicates success; otherwise, <c>false</c>.</returns>
    public static bool IsSuccess(this ResultState status)
    {
        return status == ResultState.Success || status == ResultState.NoContent || status == ResultState.Created;
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
        Guard.Against.ArgumentTypeMatch<Exception>(value!);
        return new Result<T>(value);
    }

    /// <summary>
    /// Creates a successful result containing the specified value.
    /// </summary>
    /// <typeparam name="T">The type of the value contained in the result.</typeparam>
    /// <param name="value">The value to be encapsulated in the successful result.</param>
    /// <returns>A <see cref="Result{T}"/> containing the specified value.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the specified value is an <see cref="Exception"/>. 
    /// This method only accepts non-<see cref="Exception"/> types for success results.</exception>
    public static Result<T> Created<T>(this T value)
    {
        Guard.Against.ArgumentTypeMatch<Exception>(value!);
        return new Result<T>(ResultState.Created, value);
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
        Guard.Against.Null(exception, nameof(exception));
        return Result.Failure(ResultState.Failure, exception);
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
        Guard.Against.Null(exception, nameof(exception));
        return new Result<T>(ResultState.Failure, exception);
    }
}


