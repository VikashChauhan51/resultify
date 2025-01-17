﻿namespace ResultifyCore;

/// <summary>
/// Represents the result of an operation that can succeed or fail.
/// </summary>
/// <typeparam name="T">The type of the value.</typeparam>
public readonly struct Result<T> : IEquatable<Result<T>>, IComparable<Result<T>>
{
    private readonly ResultState state;
    private readonly T? value;
    private readonly Exception? exception;

    /// <summary>
    /// Gets a value indicating whether the result is successful.
    /// </summary>
    public bool IsSuccess => exception is null;

    /// <summary>
    /// Gets the value of the result if it is successful.
    /// </summary>
    public T? Value => value;

    /// <summary>
    /// Gets the exception of the result if it is a failure.
    /// </summary>
    public Exception? Exception => exception;

    /// <summary>
    /// Initializes a new instance of the <see cref="Result{T}"/> struct with a successful value.
    /// </summary>
    /// <param name="value">The value of the successful result.</param>
    public Result(T value)
    {
        state = ResultState.Success;
        exception = null;
        this.value = value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Result{T}"/> struct with a failure exception.
    /// </summary>
    /// <param name="error">The exception of the failed result.</param>
    public Result(Exception error)
    {
        exception = error;
        state = ResultState.Failed;
        value = default;
    }

    /// <summary>
    /// Creates a successful result.
    /// </summary>
    /// <param name="value">The value of the successful result.</param>
    /// <returns>A successful <see cref="Result{T}"/>.</returns>
    public static Result<T> Success(T value) => new Result<T>(value);

    /// <summary>
    /// Creates a failed result.
    /// </summary>
    /// <param name="error">The exception of the failed result.</param>
    /// <returns>A failed <see cref="Result{T}"/>.</returns>
    public static Result<T> Failure(Exception error) => new Result<T>(error);

    /// <summary>
    /// Compares this instance with another <see cref="Result{T}"/> and returns an integer that indicates whether this instance precedes, follows, or occurs in the same position in the sort order as the other <see cref="Result{T}"/>.
    /// </summary>
    /// <param name="other">A <see cref="Result{T}"/> to compare with this instance.</param>
    /// <returns>A value that indicates the relative order of the objects being compared.</returns>
    public int CompareTo(Result<T> other)
    {
        if (IsSuccess && other.IsSuccess)
        {
            return Comparer<T>.Default.Compare(value, other.value);
        }
        if (IsSuccess)
        {
            return 1;
        }
        if (other.IsSuccess)
        {
            return -1;
        }
        return 0;
    }

    /// <summary>
    /// Determines whether this instance and another specified <see cref="Result{T}"/> have the same value or exception.
    /// </summary>
    /// <param name="other">The <see cref="Result{T}"/> to compare to this instance.</param>
    /// <returns><c>true</c> if the value or exception of the specified <see cref="Result{T}"/> is equal to the value or exception of this instance; otherwise, <c>false</c>.</returns>
    public bool Equals(Result<T> other)
    {
        if (IsSuccess && other.IsSuccess)
        {
            return EqualityComparer<T>.Default.Equals(value, other.value);
        }
        if (!IsSuccess && !other.IsSuccess)
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
    public static implicit operator Result<T>(Exception error) => new Result<T>(error);

    /// <summary>
    /// Executes one of the specified functions based on whether the result is successful or failed.
    /// </summary>
    /// <typeparam name="TResult">The type of the result produced by the functions.</typeparam>
    /// <param name="onSuccess">The function to execute if the result is successful.</param>
    /// <param name="onFailure">The function to execute if the result is failed.</param>
    /// <returns>The result of the executed function.</returns>
    public TResult Match<TResult>(Func<T, TResult> onSuccess, Func<Exception, TResult> onFailure)
    {
        return IsSuccess ? onSuccess(value!) : onFailure(exception!);
    }


    /// <summary>
    /// Executes one of the specified functions based on whether the result is successful or failed.
    /// </summary>
    /// <param name="onSuccess">The function to execute if the result is successful.</param>
    /// <param name="onFailure">The function to execute if the result is failed.</param>
    public void Match(Action<T> onSuccess, Action<Exception> onFailure)
    {
        if (IsSuccess)
        {
            onSuccess(value!);
        }
        else
        {
            onFailure(exception!);
        }
    }

    /// <summary>
    /// Executes the specified action if the result is successful.
    /// </summary>
    /// <param name="onSuccess">The action to execute if the result is successful.</param>
    public void OnSuccess(Action<T> onSuccess)
    {
        if (IsSuccess)
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
        if (!IsSuccess)
        {
            onFailure(exception!);
        }
    }

    /// <summary>
    ///  Unwrap the value if present.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public T? Unwrap() => IsSuccess ? value : throw new ResultFailureException();

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

    /// <summary>
    /// Determines whether one <see cref="Result{T}"/> instance precedes another in the sort order.
    /// </summary>
    /// <param name="left">The first <see cref="Result{T}"/> to compare.</param>
    /// <param name="right">The second <see cref="Result{T}"/> to compare.</param>
    /// <returns><c>true</c> if the first <see cref="Result{T}"/> precedes the second in the sort order; otherwise, <c>false</c>.</returns>
    public static bool operator <(Result<T> left, Result<T> right)
    {
        return left.CompareTo(right) < 0;
    }

    /// <summary>
    /// Determines whether one <see cref="Result{T}"/> instance precedes or is equal to another in the sort order.
    /// </summary>
    /// <param name="left">The first <see cref="Result{T}"/> to compare.</param>
    /// <param name="right">The second <see cref="Result{T}"/> to compare.</param>
    /// <returns><c>true</c> if the first <see cref="Result{T}"/> precedes or is equal to the second in the sort order; otherwise, <c>false</c>.</returns>
    public static bool operator <=(Result<T> left, Result<T> right)
    {
        return left.CompareTo(right) <= 0;
    }

    /// <summary>
    /// Determines whether one <see cref="Result{T}"/> instance follows another in the sort order.
    /// </summary>
    /// <param name="left">The first <see cref="Result{T}"/> to compare.</param>
    /// <param name="right">The second <see cref="Result{T}"/> to compare.</param>
    /// <returns><c>true</c> if the first <see cref="Result{T}"/> follows the second in the sort order; otherwise, <c>false</c>.</returns>
    public static bool operator >(Result<T> left, Result<T> right)
    {
        return left.CompareTo(right) > 0;
    }

    /// <summary>
    /// Determines whether one <see cref="Result{T}"/> instance follows or is equal to another in the sort order.
    /// </summary>
    /// <param name="left">The first <see cref="Result{T}"/> to compare.</param>
    /// <param name="right">The second <see cref="Result{T}"/> to compare.</param>
    /// <returns><c>true</c> if the first <see cref="Result{T}"/> follows or is equal to the second in the sort order; otherwise, <c>false</c>.</returns>
    public static bool operator >=(Result<T> left, Result<T> right)
    {
        return left.CompareTo(right) >= 0;
    }
}



/// <summary>
/// Represents the result of an operation that can succeed or fail.
/// </summary>
public readonly struct Result : IEquatable<Result>
{
    private readonly Exception? exception;

    /// <summary>
    /// Gets a value indicating whether the result is successful.
    /// </summary>
    public bool IsSuccess => exception is null;

    /// <summary>
    /// Gets the exception of the result if it is a failure.
    /// </summary>
    public Exception? Exception => exception;

    /// <summary>
    /// Initializes a new instance of the <see cref="Result"/> struct with a successful state.
    /// </summary>
    private Result(bool isSuccess)
    {
        exception = null;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Result"/> struct with a failure exception.
    /// </summary>
    /// <param name="error">The exception of the failed result.</param>
    private Result(Exception error)
    {
        exception = error;
    }

    /// <summary>
    /// Creates a successful result.
    /// </summary>
    /// <returns>A successful <see cref="Result"/>.</returns>
    public static Result Success() => new Result(true);

    /// <summary>
    /// Creates a failed result.
    /// </summary>
    /// <param name="error">The exception of the failed result.</param>
    /// <returns>A failed <see cref="Result"/>.</returns>
    public static Result Failure(Exception error) => new Result(error);

    /// <summary>
    /// Executes one of the specified actions based on whether the result is successful or failed.
    /// </summary>
    /// <param name="onSuccess">The action to execute if the result is successful.</param>
    /// <param name="onFailure">The action to execute if the result is failed.</param>
    public void Match(Action onSuccess, Action<Exception> onFailure)
    {
        if (IsSuccess)
        {
            onSuccess();
        }
        else
        {
            onFailure(exception!);
        }
    }

    /// <summary>
    /// Executes the specified action if the result is successful.
    /// </summary>
    /// <param name="onSuccess">The action to execute if the result is successful.</param>
    public void OnSuccess(Action onSuccess)
    {
        if (IsSuccess)
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
        if (!IsSuccess)
        {
            onFailure(exception!);
        }
    }

    /// <summary>
    /// Determines whether this instance and another specified <see cref="Result"/> are equal.
    /// </summary>
    /// <param name="other">The <see cref="Result"/> to compare to this instance.</param>
    /// <returns><c>true</c> if the two <see cref="Result"/> instances are equal; otherwise, <c>false</c>.</returns>
    public bool Equals(Result other)
    {
        if (IsSuccess && other.IsSuccess)
        {
            return true;
        }
        if (!IsSuccess && !other.IsSuccess)
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
}
