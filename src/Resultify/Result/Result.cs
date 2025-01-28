namespace ResultifyCore;

/// <summary>
/// Represents the result of an operation that can succeed or fail.
/// </summary>
public readonly struct Result : IEquatable<Result>
{
    private readonly ResultState state;

    private readonly Exception? exception;

    /// <summary>
    /// Gets a value indicating whether the result is successful.
    /// </summary>
    public ResultState Status => state;

    /// <summary>
    /// Gets the exception of the result if it is a failure.
    /// </summary>
    public Exception? Exception => exception;

    /// <summary>
    /// Initializes a new instance of the <see cref="Result"/> struct with a successful state.
    /// </summary>
    public Result()
    {
        exception = null;
        this.state = ResultState.Success;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Result"/> struct with a failure exception.
    /// </summary>
    ///  <param name="state">The state of the failed result.</param>
    /// <param name="error">The exception of the failed result.</param>
    private Result(ResultState state, Exception error)
    {
        exception = error;
        this.state = state;
    }

    /// <summary>
    /// Creates a successful result.
    /// </summary>
    /// <returns>A successful <see cref="Result"/>.</returns>
    public static Result Success() => new Result();

    /// <summary>
    /// Creates a successful result.
    /// </summary>
    /// <returns>A successful <see cref="Result"/>.</returns>
    public static Result Created() => new Result(ResultState.Created, null!);

    /// <summary>
    /// Creates a successful result.
    /// </summary>
    /// <returns>A successful <see cref="Result"/>.</returns>
    public static Result NoContent() => new Result(ResultState.NoContent, null!);

    /// <summary>
    /// Creates a failed result.
    /// </summary>
    /// <param name="error">The exception of the failed result.</param>
    /// <returns>A failed <see cref="Result"/>.</returns>
    public static Result Failure(ResultState state, Exception error) => new Result(state, error);

    /// <summary>
    /// Executes one of the specified actions based on whether the result is successful or failed.
    /// </summary>
    /// <param name="onSuccess">The action to execute if the result is successful.</param>
    /// <param name="onFailure">The action to execute if the result is failed.</param>
    public void Match(Action onSuccess, Action<Exception> onFailure)
    {
        if (IsSuccess())
        {
            onSuccess();
        }
        else
        {
            onFailure(exception!);
        }
    }

    /// <summary>
    /// Executes one of the specified actions based on whether the result is successful or failed.
    /// </summary>
    /// <param name="onSuccess">The action to execute if the result is successful.</param>
    /// <param name="onFailure">The action to execute if the result is failed.</param>
    public void Match(Action onSuccess, Action<ResultState, Exception> onFailure)
    {
        if (IsSuccess())
        {
            onSuccess();
        }
        else
        {
            onFailure(state, exception!);
        }
    }



    /// <summary>
    /// Executes the specified action if the result is successful.
    /// </summary>
    /// <param name="onSuccess">The action to execute if the result is successful.</param>
    public void OnSuccess(Action onSuccess)
    {
        if (IsSuccess())
        {
            onSuccess();
        }
    }

    /// <summary>
    /// Executes the specified action if the result is failed.
    /// </summary>
    /// <param name="onFailure">The action to execute if the result is failed.</param>
    public void OnFailure(Action<Exception> onFailure)
    {
        if (!IsSuccess())
        {
            onFailure(exception!);
        }
    }

    /// <summary>
    /// Executes the specified action if the result is failed.
    /// </summary>
    /// <param name="onFailure">The action to execute if the result is failed.</param>
    public void OnFailure(Action<ResultState, Exception> onFailure)
    {
        if (!IsSuccess())
        {
            onFailure(state, exception!);
        }
    }

    /// <summary>
    /// Determines whether this instance and another specified <see cref="Result"/> are equal.
    /// </summary>
    /// <param name="other">The <see cref="Result"/> to compare to this instance.</param>
    /// <returns><c>true</c> if the two <see cref="Result"/> instances are equal; otherwise, <c>false</c>.</returns>
    public bool Equals(Result other)
    {
        if (IsSuccess() && other.Status.IsSuccess())
        {
            return true;
        }
        if (!IsSuccess() && !other.Status.IsSuccess())
        {
            return EqualityComparer<Exception>.Default.Equals(exception, other.exception);
        }
        return false;
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return obj is Result other && Equals(other);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return HashCode.Combine(exception);
    }

    /// <summary>
    /// Determines whether two <see cref="Result"/> instances are equal.
    /// </summary>
    /// <param name="left">The first <see cref="Result"/> to compare.</param>
    /// <param name="right">The second <see cref="Result"/> to compare.</param>
    /// <returns><c>true</c> if the two <see cref="Result"/> instances are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(Result left, Result right) => left.Equals(right);

    /// <summary>
    /// Determines whether two <see cref="Result"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="Result"/> to compare.</param>
    /// <param name="right">The second <see cref="Result"/> to compare.</param>
    /// <returns><c>true</c> if the two <see cref="Result"/> instances are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(Result left, Result right) => !left.Equals(right);

    private bool IsSuccess()
    {
        return Status == ResultState.Success || Status == ResultState.NoContent || Status == ResultState.Created;
    }
}
