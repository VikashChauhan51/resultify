using System.Diagnostics.CodeAnalysis;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ResultifyCore;

public class Outcome : IEqualityComparer<Outcome>
{
    public OutcomeStatus Status { get; }
    public IEnumerable<OutcomeError> Errors { get; } = [];

    public Outcome(OutcomeStatus status) : this(status, [])
    {
    }

    public Outcome(IEnumerable<OutcomeError> errors) : this(OutcomeStatus.Failure, errors)
    {
        if (errors == null || !errors.Any())
        {
            throw new ArgumentException("At least one error must be provided.", nameof(errors));
        }
    }

    private Outcome(OutcomeStatus status, IEnumerable<OutcomeError> errors)
    {
        Status = status;
        Errors = errors ?? [];
    }

    public static Outcome Success()
    {
        return new Outcome(OutcomeStatus.Success, []);
    }
    public static Outcome Failure(params OutcomeError[] errors)
    {
        if (errors == null || errors.Length == 0)
        {
            throw new ArgumentException("At least one error must be provided.", nameof(errors));
        }

        return new Outcome(OutcomeStatus.Failure, errors);
    }
    public static Outcome Failure(IEnumerable<OutcomeError> errors)
    {
        if (errors == null || !errors.Any())
        {
            throw new ArgumentException("At least one error must be provided.", nameof(errors));
        }

        return new Outcome(OutcomeStatus.Failure, errors);
    }
    public static Outcome Conflict(params OutcomeError[] errors)
    {
        if (errors == null || errors.Length == 0)
        {
            throw new ArgumentException("At least one error must be provided.", nameof(errors));
        }

        return new Outcome(OutcomeStatus.Conflict, errors);
    }
    public static Outcome Conflict(IEnumerable<OutcomeError> errors)
    {
        if (errors == null || !errors.Any())
        {
            throw new ArgumentException("At least one error must be provided.", nameof(errors));
        }

        return new Outcome(OutcomeStatus.Conflict, errors);
    }
    public static Outcome Problem(params OutcomeError[] errors)
    {
        if (errors == null || errors.Length == 0)
        {
            throw new ArgumentException("At least one error must be provided.", nameof(errors));
        }

        return new Outcome(OutcomeStatus.Problem, errors);
    }
    public static Outcome Problem(IEnumerable<OutcomeError> errors)
    {
        if (errors == null || !errors.Any())
        {
            throw new ArgumentException("At least one error must be provided.", nameof(errors));
        }

        return new Outcome(OutcomeStatus.Problem, errors);
    }
    public static Outcome Validation(params OutcomeError[] errors)
    {
        if (errors == null || errors.Length == 0)
        {
            throw new ArgumentException("At least one error must be provided.", nameof(errors));
        }

        return new Outcome(OutcomeStatus.Validation, errors);
    }
    public static Outcome Validation(IEnumerable<OutcomeError> errors)
    {
        if (errors == null || !errors.Any())
        {
            throw new ArgumentException("At least one error must be provided.", nameof(errors));
        }

        return new Outcome(OutcomeStatus.Validation, errors);
    }
    public static Outcome NotFound(params OutcomeError[] errors)
    {
        if (errors == null || errors.Length == 0)
        {
            throw new ArgumentException("At least one error must be provided.", nameof(errors));
        }

        return new Outcome(OutcomeStatus.NotFound, errors);
    }
    public static Outcome NotFound(IEnumerable<OutcomeError> errors)
    {
        if (errors == null || !errors.Any())
        {
            throw new ArgumentException("At least one error must be provided.", nameof(errors));
        }

        return new Outcome(OutcomeStatus.NotFound, errors);
    }
    public static Outcome Unauthorized(params OutcomeError[] errors)
    {
        if (errors == null || errors.Length == 0)
        {
            throw new ArgumentException("At least one error must be provided.", nameof(errors));
        }

        return new Outcome(OutcomeStatus.Unauthorized, errors);
    }
    public static Outcome Unauthorized(IEnumerable<OutcomeError> errors)
    {
        if (errors == null || !errors.Any())
        {
            throw new ArgumentException("At least one error must be provided.", nameof(errors));
        }

        return new Outcome(OutcomeStatus.Unauthorized, errors);
    }

    public void Match(Action onSuccess, Action<OutcomeStatus, IEnumerable<OutcomeError>> onFailure)
    {
        if (Status == OutcomeStatus.Success)
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
        if (Status == OutcomeStatus.Success)
        {
            onSuccess();
        }
        else
        {
            onFailure(Errors);
        }
    }
    public TResult Match<TResult>(Func<TResult> onSuccess, Func<OutcomeStatus, IEnumerable<OutcomeError>, TResult> onFailure)
    {
        return Status == OutcomeStatus.Success ? onSuccess() : onFailure(Status, Errors!);
    }
    public TResult Match<TResult>(Func<TResult> onSuccess, Func<IEnumerable<OutcomeError>, TResult> onFailure)
    {
        return Status == OutcomeStatus.Success ? onSuccess() : onFailure(Errors!);
    }
    public bool Equals(Outcome? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;

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

    // IEqualityComparer implementation
    public bool Equals(Outcome? x, Outcome? y)
    {
        if (x is null || y is null) return false;
        return x.Equals(y);
    }

    public int GetHashCode([DisallowNull] Outcome obj)
    {
        return obj.GetHashCode();
    }
}


public class Outcome<T> : IEqualityComparer<Outcome<T>>
{
    public OutcomeStatus Status { get; }
    public T? Value { get; }
    public IEnumerable<OutcomeError> Errors { get; } = [];

    public Outcome(T value) : this(OutcomeStatus.Success, value, [])
    {
    }

    public Outcome(IEnumerable<OutcomeError> errors) : this(OutcomeStatus.Failure, default!, errors)
    {
        if (errors == null || !errors.Any())
        {
            throw new ArgumentException("At least one error must be provided.", nameof(errors));
        }
    }

    private Outcome(OutcomeStatus status, T value, IEnumerable<OutcomeError> errors)
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

        return new Outcome<T>(OutcomeStatus.Success, value, []);
    }

    public static Outcome<T> Failure(params OutcomeError[] errors)
    {
        if (errors == null || errors.Length == 0)
        {
            throw new ArgumentException("At least one error must be provided.", nameof(errors));
        }

        return new Outcome<T>(OutcomeStatus.Failure, default!, errors);
    }

    public static Outcome<T> Failure(IEnumerable<OutcomeError> errors)
    {
        if (errors == null || !errors.Any())
        {
            throw new ArgumentException("At least one error must be provided.", nameof(errors));
        }

        return new Outcome<T>(OutcomeStatus.Failure, default!, errors);
    }

    public static Outcome<T> Conflict(params OutcomeError[] errors)
    {
        if (errors == null || errors.Length == 0)
        {
            throw new ArgumentException("At least one error must be provided.", nameof(errors));
        }

        return new Outcome<T>(OutcomeStatus.Conflict, default!, errors);
    }
    public static Outcome<T> Conflict(IEnumerable<OutcomeError> errors)
    {
        if (errors == null || !errors.Any())
        {
            throw new ArgumentException("At least one error must be provided.", nameof(errors));
        }

        return new Outcome<T>(OutcomeStatus.Conflict, default!, errors);
    }
    public static Outcome<T> Problem(params OutcomeError[] errors)
    {
        if (errors == null || errors.Length == 0)
        {
            throw new ArgumentException("At least one error must be provided.", nameof(errors));
        }

        return new Outcome<T>(OutcomeStatus.Problem, default!, errors);
    }
    public static Outcome<T> Problem(IEnumerable<OutcomeError> errors)
    {
        if (errors == null || !errors.Any())
        {
            throw new ArgumentException("At least one error must be provided.", nameof(errors));
        }

        return new Outcome<T>(OutcomeStatus.Problem, default!, errors);
    }
    public static Outcome<T> Validation(params OutcomeError[] errors)
    {
        if (errors == null || errors.Length == 0)
        {
            throw new ArgumentException("At least one error must be provided.", nameof(errors));
        }

        return new Outcome<T>(OutcomeStatus.Validation, default!, errors);
    }
    public static Outcome<T> Validation(IEnumerable<OutcomeError> errors)
    {
        if (errors == null || !errors.Any())
        {
            throw new ArgumentException("At least one error must be provided.", nameof(errors));
        }

        return new Outcome<T>(OutcomeStatus.Validation, default!, errors);
    }
    public static Outcome<T> NotFound(params OutcomeError[] errors)
    {
        if (errors == null || errors.Length == 0)
        {
            throw new ArgumentException("At least one error must be provided.", nameof(errors));
        }

        return new Outcome<T>(OutcomeStatus.NotFound, default!, errors);
    }
    public static Outcome<T> NotFound(IEnumerable<OutcomeError> errors)
    {
        if (errors == null || !errors.Any())
        {
            throw new ArgumentException("At least one error must be provided.", nameof(errors));
        }

        return new Outcome<T>(OutcomeStatus.NotFound, default!, errors);
    }
    public static Outcome<T> Unauthorized(params OutcomeError[] errors)
    {
        if (errors == null || errors.Length == 0)
        {
            throw new ArgumentException("At least one error must be provided.", nameof(errors));
        }

        return new Outcome<T>(OutcomeStatus.Unauthorized, default!, errors);
    }
    public static Outcome<T> Unauthorized(IEnumerable<OutcomeError> errors)
    {
        if (errors == null || !errors.Any())
        {
            throw new ArgumentException("At least one error must be provided.", nameof(errors));
        }

        return new Outcome<T>(OutcomeStatus.Unauthorized, default!, errors);
    }
    public T Unwrap()
    {
        if (Status != OutcomeStatus.Success)
        {
            throw new InvalidOperationException("Cannot unwrap a failed outcome.");
        }

        return Value!;
    }

    public void Match(Action onSuccess, Action<OutcomeStatus, IEnumerable<OutcomeError>> onFailure)
    {
        if (Status == OutcomeStatus.Success)
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
        if (Status == OutcomeStatus.Success)
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
        return Status == OutcomeStatus.Success ? onSuccess(Value!) : onFailure(Errors);
    }

    public bool Equals(Outcome<T>? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;

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
}

