using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace ResultifyCore;

public class GuardClause : IGuardClause
{
    /// <inheritdoc />
    public IGuardClause Null(object? value, string paramName)
    {
        if (value is null)
            throw new ArgumentNullException(paramName, $"{paramName} cannot be null.");
        return this;
    }

    /// <inheritdoc />
    public IGuardClause NullOrEmpty(string? value, string paramName)
    {
        if (string.IsNullOrEmpty(value))
            throw new ArgumentException($"{paramName} cannot be null or empty.", paramName);
        return this;
    }

    /// <inheritdoc />
    public IGuardClause NullOrWhiteSpace(string? value, string paramName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException($"{paramName} cannot be null or whitespace.", paramName);
        return this;
    }


    /// <inheritdoc />
    public IGuardClause OutOfRange<T>(T value, T min, T max, string paramName) where T : IComparable<T>
    {
        if (value.CompareTo(min) < 0 || value.CompareTo(max) > 0)
            throw new ArgumentOutOfRangeException(paramName, $"Value must be between {min} and {max}.");
        return this;
    }

    /// <inheritdoc />
    public IGuardClause LessThan<T>(T value, T min, string paramName) where T : IComparable<T>
    {
        if (value.CompareTo(min) < 0)
            throw new ArgumentOutOfRangeException(paramName, $"{paramName} must be at least {min}.");
        return this;
    }

    /// <inheritdoc />
    public IGuardClause GreaterThan<T>(T value, T max, string paramName) where T : IComparable<T>
    {
        if (value.CompareTo(max) > 0)
            throw new ArgumentOutOfRangeException(paramName, $"{paramName} must not exceed {max}.");
        return this;
    }

    /// <inheritdoc />
    public IGuardClause NegativeOrZero(int value, string paramName)
    {
        if (value <= 0)
            throw new ArgumentOutOfRangeException(paramName, $"{paramName} must be greater than zero.");
        return this;
    }

    /// <inheritdoc />
    public IGuardClause NegativeOrZero(double value, string paramName)
    {
        if (value <= 0)
            throw new ArgumentOutOfRangeException(paramName, $"{paramName} must be greater than zero.");
        return this;
    }

    /// <inheritdoc />
    public IGuardClause NotEqual<T>(T value, T expected, string paramName)
    {
        if (EqualityComparer<T>.Default.Equals(value, expected))
            throw new ArgumentException($"{paramName} must not be equal to {expected}.", paramName);
        return this;
    }

    /// <inheritdoc />
    public IGuardClause InvalidCondition(bool condition, string message)
    {
        if (condition)
            throw new ArgumentException(message);
        return this;
    }

    /// <inheritdoc />
    public IGuardClause InvalidFormat(string value, string pattern, string paramName)
    {
        if (!Regex.IsMatch(value, pattern))
            throw new ArgumentException($"{paramName} is in an invalid format.", paramName);
        return this;
    }

    /// <inheritdoc />
    public IGuardClause InvalidEmail(string email, string paramName)
    {
        const string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return InvalidFormat(email, emailPattern, paramName);
    }

    /// <inheritdoc />
    public IGuardClause NullOrEmptyCollection<T>(IEnumerable<T>? collection, string paramName)
    {
        if (collection is null || !collection.Any())
            throw new ArgumentException($"{paramName} cannot be null or empty.", paramName);
        return this;
    }

    /// <inheritdoc />
    public IGuardClause NotFound<T>(T? value, string paramName)
    {
        if (value is null || EqualityComparer<T>.Default.Equals(value, default))
            throw new KeyNotFoundException($"{paramName} was not found.");
        return this;
    }

    /// <inheritdoc />
    public IGuardClause InvalidDateRange(DateTime startDate, DateTime endDate, string paramName)
    {
        if (startDate > endDate)
            throw new ArgumentException($"{paramName} start date cannot be after end date.");
        return this;
    }

    /// <inheritdoc />
    public IGuardClause PastDate(DateTime date, string paramName)
    {
        if (date < DateTime.UtcNow)
            throw new ArgumentException($"{paramName} cannot be a past date.");
        return this;
    }

    /// <inheritdoc />
    public IGuardClause FutureDate(DateTime date, string paramName)
    {
        if (date > DateTime.UtcNow)
            throw new ArgumentException($"{paramName} cannot be a future date.");
        return this;
    }

    /// <inheritdoc />
    public IGuardClause InvalidEnum<TEnum>(TEnum value, string paramName) where TEnum : struct, Enum
    {
        if (!Enum.IsDefined(typeof(TEnum), value))
            throw new ArgumentException($"{paramName} is not a valid enum value.");
        return this;
    }

    /// <inheritdoc />
    public IGuardClause NullOrDefault<T>(T value, string paramName)
    {
        if (EqualityComparer<T>.Default.Equals(value, default))
            throw new ArgumentException($"{paramName} cannot be null or default.");
        return this;
    }

    /// <inheritdoc />
    public IGuardClause InvalidPredicate<T>(T value, Func<T, bool> predicate, string message)
    {
        if (!predicate(value))
            throw new ArgumentException(message);
        return this;
    }

    /// <inheritdoc />
    public IGuardClause ArgumentTypeMatch<T>(object obj, [CallerArgumentExpression(nameof(obj))] string paramName = "")
    {
        if (obj is T)
            throw new InvalidOperationException($"Cannot use an {typeof(T).Name} as a value in {paramName}.");
        return this;
    }

    /// <inheritdoc />
    public IGuardClause ArgumentTypeNotMatch<T>(object obj, [CallerArgumentExpression(nameof(obj))] string paramName = "")
    {
        if (obj is not T)
            throw new InvalidOperationException($"Cannot use an {typeof(T).Name} as a value in {paramName}.");
        return this;
    }

    /// <inheritdoc />
    public IGuardClause ArgumentContainsScripts(string value, [CallerArgumentExpression(nameof(value))] string paramName = "")
    {
        const string scriptPattern = @"<.*?>|&.*?;";
        if (Regex.IsMatch(value, scriptPattern, RegexOptions.IgnoreCase))
            throw new ArgumentException("The value contains potentially dangerous scripts.", paramName);
        return this;
    }

    /// <inheritdoc />
    public IGuardClause ArgumentContainsInjection(string value, [CallerArgumentExpression(nameof(value))] string paramName = "")
    {
        const string sqlInjectionPattern = @"[\'\""]";
        if (Regex.IsMatch(value, sqlInjectionPattern, RegexOptions.IgnoreCase))
            throw new ArgumentException("The value contains potentially dangerous characters for SQL injection.", paramName);
        return this;
    }
}

