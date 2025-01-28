using System.Diagnostics.CodeAnalysis;

namespace ResultifyCore;

public class Outcome<T> : IEquatable<Outcome<T>>
{
    public ResultState Status { get; }
    public T? Value { get; }
    public IEnumerable<OutcomeError> Errors { get; } = [];

    public Outcome(T value) : this(ResultState.Success, value, [])
    {
    }

    public Outcome(IEnumerable<OutcomeError> errors) : this(ResultState.Failure, default!, errors)
    {
    }

    private Outcome(ResultState status, T value, IEnumerable<OutcomeError> errors)
    {
        Status = status;
        Value = value;
        Errors = errors ?? [];
    }

    public static Outcome<T> Success(T value)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value), "Success value cannot be null.");
        }

        return new Outcome<T>(ResultState.Success, value, []);
    }
    public static Outcome<T> Created(T value)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value), "Success value cannot be null.");
        }

        return new Outcome<T>(ResultState.Created, value, []);
    }
    public static Outcome<T> Failure(params OutcomeError[] errors)
    {
        return new Outcome<T>(ResultState.Failure, default!, errors);
    }
    public static Outcome<T> Failure(IEnumerable<OutcomeError> errors)
    {

        return new Outcome<T>(ResultState.Failure, default!, errors);
    }
    public static Outcome<T> Conflict(params OutcomeError[] errors)
    {

        return new Outcome<T>(ResultState.Conflict, default!, errors);
    }
    public static Outcome<T> Conflict(IEnumerable<OutcomeError> errors)
    {

        return new Outcome<T>(ResultState.Conflict, default!, errors);
    }
    public static Outcome<T> Problem(params OutcomeError[] errors)
    {
        return new Outcome<T>(ResultState.Problem, default!, errors);
    }
    public static Outcome<T> Problem(IEnumerable<OutcomeError> errors)
    {

        return new Outcome<T>(ResultState.Problem, default!, errors);
    }
    public static Outcome<T> Validation(params OutcomeError[] errors)
    {
        return new Outcome<T>(ResultState.Validation, default!, errors);
    }
    public static Outcome<T> Validation(IEnumerable<OutcomeError> errors)
    {
        return new Outcome<T>(ResultState.Validation, default!, errors);
    }
    public static Outcome<T> NotFound(params OutcomeError[] errors)
    {
        return new Outcome<T>(ResultState.NotFound, default!, errors);
    }
    public static Outcome<T> NotFound(IEnumerable<OutcomeError> errors)
    {

        return new Outcome<T>(ResultState.NotFound, default!, errors);
    }
    public static Outcome<T> Unauthorized(params OutcomeError[] errors)
    {

        return new Outcome<T>(ResultState.Unauthorized, default!, errors);
    }
    public static Outcome<T> Unauthorized(IEnumerable<OutcomeError> errors)
    {

        return new Outcome<T>(ResultState.Unauthorized, default!, errors);
    }
    public static Outcome<T> Unavailable(params OutcomeError[] errors)
    {
        return new Outcome<T>(ResultState.Unavailable, default!, errors);
    }
    public static Outcome<T> Unavailable(IEnumerable<OutcomeError> errors)
    {

        return new Outcome<T>(ResultState.Unavailable, default!, errors);
    }
    public static Outcome<T> CriticalError(params OutcomeError[] errors)
    {
        return new Outcome<T>(ResultState.CriticalError, default!, errors);
    }
    public static Outcome<T> CriticalError(IEnumerable<OutcomeError> errors)
    {
        return new Outcome<T>(ResultState.CriticalError, default!, errors);
    }
    public static Outcome<T> Forbidden(params OutcomeError[] errors)
    {

        return new Outcome<T>(ResultState.Forbidden, default!, errors);
    }
    public static Outcome<T> Forbidden(IEnumerable<OutcomeError> errors)
    {
        return new Outcome<T>(ResultState.Forbidden, default!, errors);
    }
    public T Unwrap()
    {
        if (!IsSuccess())
        {
            throw new InvalidOperationException("Cannot unwrap a failed outcome.");
        }

        return Value!;
    }

    public void Match(Action onSuccess, Action<ResultState, IEnumerable<OutcomeError>> onFailure)
    {
        if (IsSuccess())
        {
            onSuccess();
        }
        else
        {
            onFailure(Status, Errors);
        }
    }
    public void Match(Action onSuccess, Action<IEnumerable<OutcomeError>> onFailure)
    {
        if (IsSuccess())
        {
            onSuccess();
        }
        else
        {
            onFailure(Errors);
        }
    }

    public TResult Match<TResult>(Func<T, TResult> onSuccess, Func<IEnumerable<OutcomeError>, TResult> onFailure)
    {
        return IsSuccess() ? onSuccess(Value!) : onFailure(Errors);
    }

    public TResult Match<TResult>(Func<T, TResult> onSuccess, Func<ResultState, IEnumerable<OutcomeError>, TResult> onFailure)
    {
        return IsSuccess() ? onSuccess(Value!) : onFailure(Status, Errors);
    }

    public bool Equals(Outcome<T>? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;

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
            return (Value is null && other.Value is null) ||
                (Value is not null && other.Value is not null &&
                EqualityComparer<T>.Default.Equals(Value, other.Value));
        }

        return Status == other.Status &&
               EqualityComparer<T>.Default.Equals(Value, other.Value) &&
               Errors.SequenceEqual(other.Errors);
    }

    public override bool Equals(object? obj) => obj is Outcome<T> other && Equals(other);

    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(Status);
        hash.Add(Value);
        foreach (var error in Errors)
        {
            hash.Add(error);
        }
        return hash.ToHashCode();
    }

    public bool Equals(Outcome<T>? x, Outcome<T>? y)
    {
        if (x is null || y is null) return false;
        return x.Equals(y);
    }

    public int GetHashCode([DisallowNull] Outcome<T> obj)
    {
        return obj.GetHashCode();
    }


    /// <summary>
    /// Determines whether two <see cref="Outcome{T}"/> instances are equal.
    /// </summary>
    /// <param name="left">The first <see cref="Outcome{T}"/> to compare.</param>
    /// <param name="right">The second <see cref="Outcome{T}"/> to compare.</param>
    /// <returns><c>true</c> if the two <see cref="Outcome{T}"/> instances are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(Outcome<T> left, Outcome<T> right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two <see cref="Outcome{T}"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="Outcome{T}"/> to compare.</param>
    /// <param name="right">The second <see cref="Outcome{T}"/> to compare.</param>
    /// <returns><c>true</c> if the two <see cref="Outcome{T}"/> instances are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(Outcome<T> left, Outcome<T> right)
    {
        return !(left == right);
    }
    private bool IsSuccess()
    {
        return Status == ResultState.Success || Status == ResultState.Created;
    }
}
