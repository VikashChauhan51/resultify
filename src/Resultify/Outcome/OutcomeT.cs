using System.Diagnostics.CodeAnalysis;

namespace ResultifyCore;


/// <summary>
/// Represents the outcome of an operation, including its status, value, and any errors.
/// </summary>
/// <typeparam name="T">The type of the value associated with the outcome.</typeparam>
public class Outcome<T> : IEquatable<Outcome<T>>
{
    /// <summary>
    /// Gets the status of the outcome.
    /// </summary>
    public ResultState Status { get; }

    /// <summary>
    /// Gets the value associated with the outcome.
    /// </summary>
    public T? Value { get; }

    /// <summary>
    /// Gets the collection of errors associated with the outcome.
    /// </summary>
    public IEnumerable<OutcomeError> Errors { get; } = [];

    /// <summary>
    /// Initializes a new instance of the <see cref="Outcome{T}"/> class with a successful result.
    /// </summary>
    /// <param name="value">The value associated with the successful outcome.</param>

    public Outcome(T value) : this(ResultState.Success, value, [])
    {
    }


    /// <summary>
    /// Initializes a new instance of the <see cref="Outcome{T}"/> class with a failure result.
    /// </summary>
    /// <param name="errors">The errors associated with the failed outcome.</param>
    public Outcome(IEnumerable<OutcomeError> errors) : this(ResultState.Failure, default!, errors)
    {
    }

    private Outcome(ResultState status, T value, IEnumerable<OutcomeError> errors)
    {
        Status = status;
        Value = value;
        Errors = errors ?? [];
    }


    /// <summary>
    /// Creates a successful outcome with the specified value.
    /// </summary>
    /// <param name="value">The value associated with the successful outcome.</param>
    /// <returns>A successful <see cref="Outcome{T}"/> instance.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the value is null.</exception>
    public static Outcome<T> Success(T value)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value), "Success value cannot be null.");
        }

        return new Outcome<T>(ResultState.Success, value, []);
    }

    /// <summary>
    /// Creates a created outcome with the specified value.
    /// </summary>
    /// <param name="value">The value associated with the created outcome.</param>
    /// <returns>A created <see cref="Outcome{T}"/> instance.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the value is null.</exception>
    public static Outcome<T> Created(T value)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value), "Success value cannot be null.");
        }

        return new Outcome<T>(ResultState.Created, value, []);
    }

    /// <summary>
    /// Creates a failure outcome with the specified errors.
    /// </summary>
    /// <param name="errors">The errors associated with the failed outcome.</param>
    /// <returns>A failed <see cref="Outcome{T}"/> instance.</returns>
    public static Outcome<T> Failure(params OutcomeError[] errors)
    {
        return new Outcome<T>(ResultState.Failure, default!, errors);
    }

    /// <summary>
    /// Creates a failure outcome with the specified errors.
    /// </summary>
    /// <param name="errors">The errors associated with the failed outcome.</param>
    /// <returns>A failed <see cref="Outcome{T}"/> instance.</returns>
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

    /// <summary>
    /// Unwraps the value of a successful outcome.
    /// </summary>
    /// <returns>The value associated with the successful outcome.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the outcome is not successful.</exception>
    public T Unwrap()
    {
        if (!IsSuccess())
        {
            throw new InvalidOperationException("Cannot unwrap a failed outcome.");
        }

        return Value!;
    }

    /// <summary>
    /// Matches the outcome and executes the appropriate action based on its status.
    /// </summary>
    /// <param name="onSuccess">The action to execute if the outcome is successful.</param>
    /// <param name="onFailure">The action to execute if the outcome is a failure.</param>
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

    /// <summary>
    /// Matches the outcome and executes the appropriate action based on its status.
    /// </summary>
    /// <param name="onSuccess">The action to execute if the outcome is successful.</param>
    /// <param name="onFailure">The action to execute if the outcome is a failure.</param>
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


    /// <summary>
    /// Matches the outcome and returns the appropriate result based on its status.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="onSuccess">The function to execute if the outcome is successful.</param>
    /// <param name="onFailure">The function to execute if the outcome is a failure.</param>
    /// <returns>The result of the appropriate function based on the outcome's status.</returns>
    public TResult Match<TResult>(Func<T, TResult> onSuccess, Func<IEnumerable<OutcomeError>, TResult> onFailure)
    {
        return IsSuccess() ? onSuccess(Value!) : onFailure(Errors);
    }


    /// <summary>
    /// Matches the outcome and returns the appropriate result based on its status.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="onSuccess">The function to execute if the outcome is successful.</param>
    /// <param name="onFailure">The function to execute if the outcome is a failure.</param>
    /// <returns>The result of the appropriate function based on the outcome's status.</returns>
    public TResult Match<TResult>(Func<T, TResult> onSuccess, Func<ResultState, IEnumerable<OutcomeError>, TResult> onFailure)
    {
        return IsSuccess() ? onSuccess(Value!) : onFailure(Status, Errors);
    }

    /// <summary>
    /// Determines whether the specified <see cref="Outcome{T}"/> is equal to the current <see cref="Outcome{T}"/>.
    /// </summary>
    /// <param name="other">The <see cref="Outcome{T}"/> to compare with the current <see cref="Outcome{T}"/>.</param>
    /// <returns><c>true</c> if the specified <see cref="Outcome{T}"/> is equal to the current <see cref="Outcome{T}"/>; otherwise, <c>false</c>.</returns>
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

    /// <summary>
    /// Determines whether the specified object is equal to the current <see cref="Outcome{T}"/>.
    /// </summary>
    /// <param name="obj">The object to compare with the current <see cref="Outcome{T}"/>.</param>
    /// <returns><c>true</c> if the specified object is equal to the current <see cref="Outcome{T}"/>; otherwise, <c>false</c>.</returns>
    public override bool Equals(object? obj) => obj is Outcome<T> other && Equals(other);


    /// <summary>
    /// Serves as the default hash function.
    /// </summary>
    /// <returns>A hash code for the current <see cref="Outcome{T}"/>.</returns>
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


    /// <summary>
    /// Determines whether two <see cref="Outcome{T}"/> instances are equal.
    /// </summary>
    /// <param name="x">The first <see cref="Outcome{T}"/> to compare.</param>
    /// <param name="y">The second <see cref="Outcome{T}"/> to compare.</param>
    /// <returns><c>true</c> if the two <see cref="Outcome{T}"/> instances are equal; otherwise, <c>false</c>.</returns>
    public bool Equals(Outcome<T>? x, Outcome<T>? y)
    {
        if (x is null || y is null) return false;
        return x.Equals(y);
    }


    /// <summary>
    /// Returns a hash code for the specified <see cref="Outcome{T}"/>.
    /// </summary>
    /// <param name="obj">The <see cref="Outcome{T}"/> for which a hash code is to be returned.</param>
    /// <returns>A hash code for the specified <see cref="Outcome{T}"/>.</returns>
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
