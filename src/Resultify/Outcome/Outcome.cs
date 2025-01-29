using System.Diagnostics.CodeAnalysis;

namespace ResultifyCore;


/// <summary>
/// Represents the outcome of an operation, which can be either successful or failed with associated errors.
/// </summary>
public sealed class Outcome : IEquatable<Outcome>
{
    /// <summary>
    /// Gets the status of the outcome.
    /// </summary>
    public ResultState Status { get; }

    /// <summary>
    /// Gets the collection of errors associated with the outcome.
    /// </summary>
    public IEnumerable<OutcomeError> Errors { get; } = [];


    /// <summary>
    /// Initializes a new instance of the <see cref="Outcome"/> class with the specified status.
    /// </summary>
    /// <param name="status">The status of the outcome.</param>
    public Outcome(ResultState status) : this(status, [])
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Outcome"/> class with the specified errors.
    /// </summary>
    /// <param name="errors">The errors associated with the outcome.</param>
    /// <exception cref="ArgumentException">Thrown when the errors collection is null or empty.</exception>
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


    /// <summary>
    /// Creates a successful outcome.
    /// </summary>
    /// <returns>A successful <see cref="Outcome"/>.</returns>
    public static Outcome Success()
    {
        return new Outcome(ResultState.Success, []);
    }

    /// <summary>
    /// Creates an outcome indicating that a resource was created.
    /// </summary>
    /// <returns>An <see cref="Outcome"/> indicating that a resource was created.</returns>
    public static Outcome Created()
    {
        return new Outcome(ResultState.Created, []);
    }

    /// <summary>
    /// Creates an outcome indicating that there is no content.
    /// </summary>
    /// <returns>An <see cref="Outcome"/> indicating that there is no content.</returns>
    public static Outcome NoContent()
    {
        return new Outcome(ResultState.NoContent, []);
    }

    /// <summary>
    /// Creates a failed outcome with the specified errors.
    /// </summary>
    /// <param name="errors">The errors associated with the failure.</param>
    /// <returns>A failed <see cref="Outcome"/>.</returns>
    public static Outcome Failure(params OutcomeError[] errors)
    {
        return new Outcome(ResultState.Failure, errors);
    }

    /// <summary>
    /// Creates a failed outcome with the specified errors.
    /// </summary>
    /// <param name="errors">The errors associated with the failure.</param>
    /// <returns>A failed <see cref="Outcome"/>.</returns>
    public static Outcome Failure(IEnumerable<OutcomeError> errors)
    {
        return new Outcome(ResultState.Failure, errors);
    }


    /// <summary>
    /// Creates a Conflict outcome with the specified errors.
    /// </summary>
    /// <param name="errors">The errors associated with the failure.</param>
    /// <returns>A failed <see cref="Outcome"/>.</returns>
    public static Outcome Conflict(params OutcomeError[] errors)
    {
        return new Outcome(ResultState.Conflict, errors);
    }

    /// <summary>
    /// Creates a Conflict outcome with the specified errors.
    /// </summary>
    /// <param name="errors">The errors associated with the failure.</param>
    /// <returns>A failed <see cref="Outcome"/>.</returns>
    public static Outcome Conflict(IEnumerable<OutcomeError> errors)
    {
        return new Outcome(ResultState.Conflict, errors);
    }

    /// <summary>
    /// Creates a Problem outcome with the specified errors.
    /// </summary>
    /// <param name="errors">The errors associated with the failure.</param>
    /// <returns>A failed <see cref="Outcome"/>.</returns>
    public static Outcome Problem(params OutcomeError[] errors)
    {
        return new Outcome(ResultState.Problem, errors);
    }

    /// <summary>
    /// Creates a Problem outcome with the specified errors.
    /// </summary>
    /// <param name="errors">The errors associated with the failure.</param>
    /// <returns>A failed <see cref="Outcome"/>.</returns>
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

    /// <summary>
    /// Matches the outcome and executes the appropriate action based on the outcome status.
    /// </summary>
    /// <param name="onSuccess">The action to execute if the outcome is successful.</param>
    /// <param name="onFailure">The action to execute if the outcome is failed.</param>
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
    /// Matches the outcome and executes the appropriate action based on the outcome status.
    /// </summary>
    /// <param name="onSuccess">The action to execute if the outcome is successful.</param>
    /// <param name="onFailure">The action to execute if the outcome is failed.</param>
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
    /// Matches the outcome and returns the appropriate result based on the outcome status.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="onSuccess">The function to execute if the outcome is successful.</param>
    /// <param name="onFailure">The function to execute if the outcome is failed.</param>
    /// <returns>The result of the executed function.</returns>
    public TResult Match<TResult>(Func<TResult> onSuccess, Func<ResultState, IEnumerable<OutcomeError>, TResult> onFailure)
    {
        return IsSuccess() ? onSuccess() : onFailure(Status, Errors!);
    }

    /// <summary>
    /// Matches the outcome and returns the appropriate result based on the outcome status.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="onSuccess">The function to execute if the outcome is successful.</param>
    /// <param name="onFailure">The function to execute if the outcome is failed.</param>
    /// <returns>The result of the executed function.</returns>
    public TResult Match<TResult>(Func<TResult> onSuccess, Func<IEnumerable<OutcomeError>, TResult> onFailure)
    {
        return IsSuccess() ? onSuccess() : onFailure(Errors!);
    }

    /// <inheritdoc/>
    public bool Equals(Outcome? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        if (IsSuccess() && other.IsSuccess()) return true;
        return Status == other.Status && Errors.SequenceEqual(other.Errors);
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is Outcome other && Equals(other);

    /// <inheritdoc/>
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

    /// <summary>
    /// Determines whether two <see cref="Outcome"/> instances are equal.
    /// </summary>
    /// <param name="x">The first <see cref="Outcome"/> to compare.</param>
    /// <param name="y">The second <see cref="Outcome"/> to compare.</param>
    /// <returns><c>true</c> if the two <see cref="Outcome"/> instances are equal; otherwise, <c>false</c>.</returns>
    public bool Equals(Outcome? x, Outcome? y)
    {
        if (x is null || y is null) return false;
        return x.Equals(y);
    }

    /// <summary>
    /// Returns a hash code for the specified <see cref="Outcome"/>.
    /// </summary>
    /// <param name="obj">The <see cref="Outcome"/> for which to get a hash code.</param>
    /// <returns>A hash code for the specified <see cref="Outcome"/>.</returns>
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




