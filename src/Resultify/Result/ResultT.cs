namespace ResultifyCore;

/// <summary>
/// Represents the result of an operation that can succeed or fail.
/// </summary>
/// <typeparam name="T">The type of the value.</typeparam>
public readonly struct Result<T> : IEquatable<Result<T>>
{
    private readonly ResultState state;
    private readonly T? value;
    private readonly Exception? exception;

    /// <summary>
    /// Gets a value indicating whether the result is successful.
    /// </summary>
    public ResultState Status => state;

    /// <summary>
    /// Gets the value of the result if it is successful.
    /// </summary>
    public T? Value => value;

    /// <summary>
    /// Gets the exception of the result if it is a failure.
    /// </summary>
    public Exception? Exception => exception;

    /// <summary>
    /// Initializes a new instance of the <see cref="Result{T}"/> struct with the specified status, value, and error.
    /// </summary>
    /// <param name="state">The state of the result.</param>
    /// <param name="value">The value of the result.</param>
    /// <param name="error">The exception of the result.</param>
    private Result(ResultState state, T? value, Exception? error)
    {
        this.state = state;
        this.value = value;
        this.exception = error;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Result{T}"/> struct with a successful value.
    /// </summary>
    /// <param name="value">The value of the successful result.</param>
    public Result(T value) : this(ResultState.Success, value, null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Result{T}"/> struct with a failure exception.
    /// </summary>
    /// <param name="state">The state of the failed result.</param>
    /// <param name="error">The exception of the failed result.</param>
    public Result(ResultState state, Exception error) : this(state, default!, error)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Result{T}"/> struct with a failure exception.
    /// </summary>
    /// <param name="state">The state of the failed result.</param>
    ///<param name="value">The value of the successful result.</param>
    public Result(ResultState state, T value) : this(state, value, null)
    {
    }

    /// <summary>
    /// Creates a successful result.
    /// </summary>
    /// <param name="value">The value of the successful result.</param>
    /// <returns>A successful <see cref="Result{T}"/>.</returns>
    public static Result<T> Success(T value) => new Result<T>(value);

    /// <summary>
    /// Creates a successful result.
    /// </summary>
    /// <param name="value">The value of the successful result.</param>
    /// <returns>A successful <see cref="Result{T}"/>.</returns>
    public static Result<T> Created(T value) => new Result<T>(ResultState.Created, value);

    /// <summary>
    /// Creates a successful result without value.
    /// </summary>
    /// <returns>A successful <see cref="Result{T}"/>.</returns>
    public static Result<T> Created() => new Result<T>(ResultState.Created, null!);

    /// <summary>
    /// Creates a successful result without value.
    /// </summary>
    /// <returns>A successful <see cref="Result{T}"/>.</returns>
    public static Result<T> NoContent() => new Result<T>(ResultState.NoContent, null!);

    /// <summary>
    ///  Creates a successful result.
    /// </summary>
    /// <param name="state">The state of the successful result.</param>
    /// <param name="value">The value of the successful result.</param>
    /// <returns>A successful <see cref="Result{T}"/>.</returns>
    public static Result<T> WithValue(ResultState state, T value) => new Result<T>(state, value);

    /// <summary>
    /// Creates a failed result.
    /// </summary>
    /// <param name="state">The state of the failed result.</param>
    /// <param name="error">The exception of the failed result.</param>
    /// <returns>A failed <see cref="Result{T}"/>.</returns>
    public static Result<T> WithError(ResultState state, Exception error) => new Result<T>(state, error);


    /// <summary>
    /// Determines whether this instance and another specified <see cref="Result{T}"/> have the same value or exception.
    /// </summary>
    /// <param name="other">The <see cref="Result{T}"/> to compare to this instance.</param>
    /// <returns><c>true</c> if the value or exception of the specified <see cref="Result{T}"/> is equal to the value or exception of this instance; otherwise, <c>false</c>.</returns>
    public bool Equals(Result<T> other)
    {
        if (Status == ResultState.Success && other.Status == ResultState.Success)
        {
            return true;
        }
        if (Status == ResultState.NoContent && other.Status == ResultState.NoContent)
        {
            return true;
        }
        if (Status == ResultState.Created && other.Status == ResultState.Created)
        {
            return (value is null && other.value is null) ||
                (value is not null && other.value is not null &&
                EqualityComparer<T>.Default.Equals(value, other.value));
        }
        if (!IsSuccess() && !other.Status.IsSuccess())
        {
            return EqualityComparer<Exception>.Default.Equals(exception, other.exception);
        }
        return false;
    }

    /// <summary>
    /// Determines whether the specified object is equal to this instance.
    /// </summary>
    /// <param name="obj">The object to compare with this instance.</param>
    /// <returns><c>true</c> if the specified object is equal to this instance; otherwise, <c>false</c>.</returns>
    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return base.Equals(obj);
        }

        return obj is Result<T> other && Equals(other);
    }

    /// <summary>
    /// Returns the hash code for this instance.
    /// </summary>
    /// <returns>A hash code for the current object.</returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(state, value, exception);
    }

    /// <summary>
    /// Implicitly converts a value to a successful <see cref="Result{T}"/> containing that value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    public static implicit operator Result<T>(T value) => new Result<T>(value);

    /// <summary>
    /// Implicitly converts an exception to a failed <see cref="Result{T}"/> containing that exception.
    /// </summary>
    /// <param name="error">The exception to convert.</param>
    public static implicit operator Result<T>(Exception error) => new Result<T>(ResultState.Failure, error);

    /// <summary>
    /// Executes one of the specified functions based on whether the result is successful or failed.
    /// </summary>
    /// <typeparam name="TResult">The type of the result produced by the functions.</typeparam>
    /// <param name="onSuccess">The function to execute if the result is successful.</param>
    /// <param name="onFailure">The function to execute if the result is failed.</param>
    /// <returns>The result of the executed function.</returns>
    public TResult Match<TResult>(Func<T, TResult> onSuccess, Func<Exception, TResult> onFailure)
    {
        return IsSuccess() ? onSuccess(value!) : onFailure(exception!);
    }


    /// <summary>
    /// Executes one of the specified functions based on whether the result is successful or failed.
    /// </summary>
    /// <typeparam name="TResult">The type of the result produced by the functions.</typeparam>
    /// <param name="onSuccess">The function to execute if the result is successful.</param>
    /// <param name="onFailure">The function to execute if the result is failed.</param>
    /// <returns>The result of the executed function.</returns>
    public TResult Match<TResult>(Func<T, TResult> onSuccess, Func<ResultState, Exception, TResult> onFailure)
    {
        return IsSuccess() ? onSuccess(value!) : onFailure(state, exception!);
    }


    /// <summary>
    /// Executes one of the specified functions based on whether the result is successful or failed.
    /// </summary>
    /// <param name="onSuccess">The function to execute if the result is successful.</param>
    /// <param name="onFailure">The function to execute if the result is failed.</param>
    public void Match(Action<T> onSuccess, Action<Exception> onFailure)
    {
        if (IsSuccess())
        {
            onSuccess(value!);
        }
        else
        {
            onFailure(exception!);
        }
    }

    /// <summary>
    /// Executes one of the specified functions based on whether the result is successful or failed.
    /// </summary>
    /// <param name="onSuccess">The function to execute if the result is successful.</param>
    /// <param name="onFailure">The function to execute if the result is failed.</param>
    public void Match(Action<T> onSuccess, Action<ResultState, Exception> onFailure)
    {
        if (IsSuccess())
        {
            onSuccess(value!);
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
    public void OnSuccess(Action<T> onSuccess)
    {
        if (IsSuccess())
        {
            onSuccess(value!);
        }
    }

    /// <summary>
    /// Executes the specified action if the result is failed.
    /// </summary>
    /// <param name="onFailure">The action to execute if the result is failed.</param>
    public void OnFailure(Action<Exception> onFailure)
    {
        if (Status != ResultState.Success)
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
        if (Status != ResultState.Success)
        {
            onFailure(state, exception!);
        }
    }

    /// <summary>
    ///  Unwrap the value if present.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public T? Unwrap() => IsSuccess() ? value : throw new ResultFailureException();

    /// <summary>
    /// Determines whether two <see cref="Result{T}"/> instances are equal.
    /// </summary>
    /// <param name="left">The first <see cref="Result{T}"/> to compare.</param>
    /// <param name="right">The second <see cref="Result{T}"/> to compare.</param>
    /// <returns><c>true</c> if the two <see cref="Result{T}"/> instances are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(Result<T> left, Result<T> right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two <see cref="Result{T}"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="Result{T}"/> to compare.</param>
    /// <param name="right">The second <see cref="Result{T}"/> to compare.</param>
    /// <returns><c>true</c> if the two <see cref="Result{T}"/> instances are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(Result<T> left, Result<T> right)
    {
        return !(left == right);
    }

    private bool IsSuccess()
    {
        return Status == ResultState.Success || Status == ResultState.NoContent || Status == ResultState.Created;
    }
}
