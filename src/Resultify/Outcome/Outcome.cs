using System.Diagnostics.CodeAnalysis;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ResultifyCore;

public sealed class Outcome : IEquatable<Outcome>
{
    public ResultState Status { get; }
    public IEnumerable<OutcomeError> Errors { get; } = [];

    public Outcome(ResultState status) : this(status, [])
    {
    }

    public Outcome(IEnumerable<OutcomeError> errors) : this(ResultState.Failure, errors)
    {
        if (errors == null || !errors.Any())
        {
            throw new ArgumentException("At least one error must be provided.", nameof(errors));
        }
    }

    private Outcome(ResultState status, IEnumerable<OutcomeError> errors)
    {
        Status = status;
        Errors = errors ?? [];
    }

    public static Outcome Success()
    {
        return new Outcome(ResultState.Success, []);
    }
    public static Outcome Created()
    {
        return new Outcome(ResultState.Created, []);
    }
    public static Outcome NoContent()
    {
        return new Outcome(ResultState.NoContent, []);
    }
    public static Outcome Failure(params OutcomeError[] errors)
    {
        return new Outcome(ResultState.Failure, errors);
    }
    public static Outcome Failure(IEnumerable<OutcomeError> errors)
    {
        return new Outcome(ResultState.Failure, errors);
    }
    public static Outcome Conflict(params OutcomeError[] errors)
    {
        return new Outcome(ResultState.Conflict, errors);
    }
    public static Outcome Conflict(IEnumerable<OutcomeError> errors)
    {
        return new Outcome(ResultState.Conflict, errors);
    }
    public static Outcome Problem(params OutcomeError[] errors)
    {
        return new Outcome(ResultState.Problem, errors);
    }
    public static Outcome Problem(IEnumerable<OutcomeError> errors)
    {
        return new Outcome(ResultState.Problem, errors);
    }
    public static Outcome Validation(params OutcomeError[] errors)
    {
        return new Outcome(ResultState.Validation, errors);
    }
    public static Outcome Validation(IEnumerable<OutcomeError> errors)
    {
        return new Outcome(ResultState.Validation, errors);
    }
    public static Outcome NotFound(params OutcomeError[] errors)
    {
        return new Outcome(ResultState.NotFound, errors);
    }
    public static Outcome NotFound(IEnumerable<OutcomeError> errors)
    {
        return new Outcome(ResultState.NotFound, errors);
    }
    public static Outcome Unauthorized(params OutcomeError[] errors)
    {
        return new Outcome(ResultState.Unauthorized, errors);
    }
    public static Outcome Unauthorized(IEnumerable<OutcomeError> errors)
    {
        return new Outcome(ResultState.Unauthorized, errors);
    }
    public static Outcome Unavailable(params OutcomeError[] errors)
    {
        return new Outcome(ResultState.Unavailable, errors);
    }
    public static Outcome Unavailable(IEnumerable<OutcomeError> errors)
    {

        return new Outcome(ResultState.Unavailable, errors);
    }
    public static Outcome CriticalError(params OutcomeError[] errors)
    {
        return new Outcome(ResultState.CriticalError, errors);
    }
    public static Outcome CriticalError(IEnumerable<OutcomeError> errors)
    {
        return new Outcome(ResultState.CriticalError, errors);
    }
    public static Outcome Forbidden(params OutcomeError[] errors)
    {

        return new Outcome(ResultState.Forbidden, errors);
    }
    public static Outcome Forbidden(IEnumerable<OutcomeError> errors)
    {
        return new Outcome(ResultState.Forbidden, errors);
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
    public TResult Match<TResult>(Func<TResult> onSuccess, Func<ResultState, IEnumerable<OutcomeError>, TResult> onFailure)
    {
        return IsSuccess() ? onSuccess() : onFailure(Status, Errors!);
    }
    public TResult Match<TResult>(Func<TResult> onSuccess, Func<IEnumerable<OutcomeError>, TResult> onFailure)
    {
        return IsSuccess() ? onSuccess() : onFailure(Errors!);
    }
    public bool Equals(Outcome? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        if (IsSuccess() && other.IsSuccess()) return true;
        return Status == other.Status && Errors.SequenceEqual(other.Errors);
    }
    public override bool Equals(object? obj) => obj is Outcome other && Equals(other);
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(Status);
        foreach (var error in Errors)
        {
            hash.Add(error);
        }
        return hash.ToHashCode();
    }

    public bool Equals(Outcome? x, Outcome? y)
    {
        if (x is null || y is null) return false;
        return x.Equals(y);
    }

    public int GetHashCode([DisallowNull] Outcome obj)
    {
        return obj.GetHashCode();
    }

    /// <summary>
    /// Determines whether two <see cref="Outcome"/> instances are equal.
    /// </summary>
    /// <param name="left">The first <see cref="Outcome"/> to compare.</param>
    /// <param name="right">The second <see cref="Outcome"/> to compare.</param>
    /// <returns><c>true</c> if the two <see cref="Outcome"/> instances are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(Outcome left, Outcome right) => left.Equals(right);

    /// <summary>
    /// Determines whether two <see cref="Outcome"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="Outcome"/> to compare.</param>
    /// <param name="right">The second <see cref="Outcome"/> to compare.</param>
    /// <returns><c>true</c> if the two <see cref="Outcome"/> instances are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(Outcome left, Outcome right) => !left.Equals(right);

    private bool IsSuccess()
    {
        return Status == ResultState.Success || Status == ResultState.NoContent || Status == ResultState.Created;
    }
}




