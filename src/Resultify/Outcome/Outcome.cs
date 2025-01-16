using System.Diagnostics.CodeAnalysis;

namespace ResultifyCore.Outcome;

public class Outcome : IEqualityComparer<Outcome>
{
    public bool IsSuccess { get; }
    public IEnumerable<OutcomeError> Errors { get; } = [];

    public Outcome(bool isSuccess) : this(isSuccess, [])
    {
    }

    public Outcome(IEnumerable<OutcomeError> errors) : this(false, errors)
    {
        if (errors == null || !errors.Any())
        {
            throw new ArgumentException("At least one error must be provided.", nameof(errors));
        }
    }

    private Outcome(bool isSuccess, IEnumerable<OutcomeError> errors)
    {
        IsSuccess = isSuccess;
        Errors = errors ?? [];
    }

    public static Outcome Success()
    {
        return new Outcome(true, []);
    }

    public static Outcome Failure(params OutcomeError[] errors)
    {
        if (errors == null || errors.Length == 0)
        {
            throw new ArgumentException("At least one error must be provided.", nameof(errors));
        }

        return new Outcome(false, errors);
    }

    public static Outcome Failure(IEnumerable<OutcomeError> errors)
    {
        if (errors == null || !errors.Any())
        {
            throw new ArgumentException("At least one error must be provided.", nameof(errors));
        }

        return new Outcome(false, errors);
    }

    public void Match(Action onSuccess, Action<IEnumerable<OutcomeError>> onFailure)
    {
        if (IsSuccess)
        {
            onSuccess();
        }
        else
        {
            onFailure(Errors);
        }
    }

    public bool Equals(Outcome? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;

        return IsSuccess == other.IsSuccess && Errors.SequenceEqual(other.Errors);
    }

    public override bool Equals(object? obj) => obj is Outcome other && Equals(other);

    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(IsSuccess);
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
    public bool IsSuccess { get; }
    public T? Value { get; }
    public IEnumerable<OutcomeError> Errors { get; } = [];

    public Outcome(T value) : this(true, value, [])
    {
    }

    public Outcome(IEnumerable<OutcomeError> errors) : this(false, default, errors)
    {
        if (errors == null || !errors.Any())
        {
            throw new ArgumentException("At least one error must be provided.", nameof(errors));
        }
    }

    private Outcome(bool isSuccess, T value, IEnumerable<OutcomeError> errors)
    {
        IsSuccess = isSuccess;
        Value = value;
        Errors = errors ?? [];
    }

    public static Outcome<T> Success(T value)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value), "Success value cannot be null.");
        }

        return new Outcome<T>(true, value, []);
    }

    public static Outcome<T> Failure(params OutcomeError[] errors)
    {
        if (errors == null || errors.Length == 0)
        {
            throw new ArgumentException("At least one error must be provided.", nameof(errors));
        }

        return new Outcome<T>(false, default, errors);
    }

    public static Outcome<T> Failure(IEnumerable<OutcomeError> errors)
    {
        if (errors == null || !errors.Any())
        {
            throw new ArgumentException("At least one error must be provided.", nameof(errors));
        }

        return new Outcome<T>(false, default, errors);
    }

    public T Unwrap()
    {
        if (!IsSuccess)
        {
            throw new InvalidOperationException("Cannot unwrap a failed outcome.");
        }

        return Value!;
    }

    public void Match(Action onSuccess, Action<IEnumerable<OutcomeError>> onFailure)
    {
        if (IsSuccess)
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
        return IsSuccess ? onSuccess(Value!) : onFailure(Errors);
    }

    public bool Equals(Outcome<T>? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;

        return IsSuccess == other.IsSuccess &&
               EqualityComparer<T>.Default.Equals(Value, other.Value) &&
               Errors.SequenceEqual(other.Errors);
    }

    public override bool Equals(object? obj) => obj is Outcome<T> other && Equals(other);

    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(IsSuccess);
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

