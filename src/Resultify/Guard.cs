using System.Runtime.CompilerServices;

namespace ResultifyCore;
public static class Guard
{
    public static void ThrowIfArgumentIsNull<T>([ValidatedNotNull][NoEnumeration] T obj,
        [CallerArgumentExpression(nameof(obj))] string paramName = "")
    {
        if (obj is null)
        {
            throw new ArgumentNullException(paramName);
        }
    }

    public static void ThrowIfArgumentIsNull<T>([ValidatedNotNull][NoEnumeration] T obj, string paramName, string message)
    {
        if (obj is null)
        {
            throw new ArgumentNullException(paramName, message);
        }
    }

    public static void ThrowIfArgumentIsNullOrEmpty([ValidatedNotNull] string str,
        [CallerArgumentExpression(nameof(str))] string paramName = "")
    {
        if (string.IsNullOrEmpty(str))
        {
            ThrowIfArgumentIsNull(str, paramName);
            throw new ArgumentException("The value cannot be an empty string.", paramName);
        }
    }

    public static void ThrowIfArgumentIsNullOrEmpty([ValidatedNotNull] string str, string paramName, string message)
    {
        if (string.IsNullOrEmpty(str))
        {
            ThrowIfArgumentIsNull(str, paramName, message);
            throw new ArgumentException(message, paramName);
        }
    }

    public static void ThrowIfArgumentIsOutOfRange<T>(T value, [CallerArgumentExpression(nameof(value))] string paramName = "")
        where T : Enum
    {
        if (!Enum.IsDefined(typeof(T), value))
        {
            throw new ArgumentOutOfRangeException(paramName);
        }
    }

    public static void ThrowIfArgumentContainsNull<T>(IEnumerable<T> values,
        [CallerArgumentExpression(nameof(values))] string paramName = "")
    {
        if (values.Any(t => t is null))
        {
            throw new ArgumentNullException(paramName, "Collection contains a null value");
        }
    }

    public static void ThrowIfArgumentIsEmpty<T>(IEnumerable<T> values, string paramName, string message)
    {
        if (!values.Any())
        {
            throw new ArgumentException(message, paramName);
        }
    }

    public static void ThrowIfArgumentIsEmpty(string str, string paramName, string message)
    {
        if (str.Length == 0)
        {
            throw new ArgumentException(message, paramName);
        }
    }

    public static void ThrowIfArgumentIsNegative(TimeSpan timeSpan,
        [CallerArgumentExpression(nameof(timeSpan))] string paramName = "")
    {
        if (timeSpan < TimeSpan.Zero)
        {
            throw new ArgumentOutOfRangeException(paramName, "The value must be non-negative.");
        }
    }

    public static void ThrowIfArgumentIsNegative(float value, [CallerArgumentExpression(nameof(value))] string paramName = "")
    {
        if (value < 0)
        {
            throw new ArgumentOutOfRangeException(paramName, "The value must be non-negative.");
        }
    }

    public static void ThrowIfArgumentIsNegative(double value, [CallerArgumentExpression(nameof(value))] string paramName = "")
    {
        if (value < 0)
        {
            throw new ArgumentOutOfRangeException(paramName, "The value must be non-negative.");
        }
    }

    public static void ThrowIfArgumentIsNegative(decimal value, [CallerArgumentExpression(nameof(value))] string paramName = "")
    {
        if (value < 0)
        {
            throw new ArgumentOutOfRangeException(paramName, "The value must be non-negative.");
        }
    }

    public static void ThrowIfArgumentIsNotInRange(int value, int minValue, int maxValue,
        [CallerArgumentExpression(nameof(value))] string paramName = "")
    {
        if (value < minValue || value > maxValue)
        {
            throw new ArgumentOutOfRangeException(paramName, $"Value must be between {minValue} and {maxValue}. Actual value: {value}");
        }
    }

    public static void ThrowIfArgumentIsNotInRange(double value, double minValue, double maxValue,
        [CallerArgumentExpression(nameof(value))] string paramName = "")
    {
        if (value < minValue || value > maxValue)
        {
            throw new ArgumentOutOfRangeException(paramName, $"Value must be between {minValue} and {maxValue}. Actual value: {value}");
        }
    }

    public static void ThrowIfArgumentDoesNotMatchPattern(string value, string pattern,
        [CallerArgumentExpression(nameof(value))] string paramName = "")
    {
        if (!System.Text.RegularExpressions.Regex.IsMatch(value, pattern))
        {
            throw new ArgumentException($"The value does not match the expected pattern: {pattern}.", paramName);
        }
    }

    public static void ThrowIfArgumentIsEmptyOrWhiteSpace([ValidatedNotNull] string str,
        [CallerArgumentExpression(nameof(str))] string paramName = "")
    {
        if (string.IsNullOrWhiteSpace(str))
        {
            throw new ArgumentException("The value cannot be null, empty, or whitespace.", paramName);
        }
    }

    public static void ThrowIfArgumentIsNegativeOrZero(int value, [CallerArgumentExpression(nameof(value))] string paramName = "")
    {
        if (value <= 0)
        {
            throw new ArgumentOutOfRangeException(paramName, "The value must be greater than zero.");
        }
    }

    public static void ThrowIfArgumentIsNegativeOrZero(decimal value, [CallerArgumentExpression(nameof(value))] string paramName = "")
    {
        if (value <= 0)
        {
            throw new ArgumentOutOfRangeException(paramName, "The value must be greater than zero.");
        }
    }

    public static void ThrowIfArgumentIsNotInEnumRange<T>(T value, [CallerArgumentExpression(nameof(value))] string paramName = "")
        where T : Enum
    {
        if (!Enum.IsDefined(typeof(T), value))
        {
            throw new ArgumentOutOfRangeException(paramName, $"Value '{value}' is not defined in the enum {typeof(T).Name}.");
        }
    }

    public static void ThrowIfArgumentIsNotInRange(DateTime value, DateTime minValue, DateTime maxValue,
        [CallerArgumentExpression(nameof(value))] string paramName = "")
    {
        if (value < minValue || value > maxValue)
        {
            throw new ArgumentOutOfRangeException(paramName, $"Value must be between {minValue} and {maxValue}. Actual value: {value}");
        }
    }

    public static void ThrowIfArgumentIsNotInRange(Guid value, Guid minValue, Guid maxValue,
        [CallerArgumentExpression(nameof(value))] string paramName = "")
    {
        int comparisonMin = value.CompareTo(minValue);
        int comparisonMax = value.CompareTo(maxValue);

        if (comparisonMin < 0 || comparisonMax > 0)
        {
            throw new ArgumentOutOfRangeException(paramName, $"Value must be between {minValue} and {maxValue}. Actual value: {value}");
        }
    }

    /// <summary>
    /// Workaround to make dotnet_code_quality.null_check_validation_methods work
    /// https://github.com/dotnet/roslyn-analyzers/issues/3451#issuecomment-606690452
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    private sealed class ValidatedNotNullAttribute : Attribute;

    [AttributeUsage(AttributeTargets.Parameter)]
    private sealed class NoEnumerationAttribute : Attribute;
}

