using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace ResultifyCore;
public static class Guard
{

    /// <summary>
    /// Throws an <see cref="ArgumentNullException"/> if the argument is null.
    /// </summary>
    /// <typeparam name="T">The type of the object being checked.</typeparam>
    /// <param name="obj">The object to check.</param>
    /// <param name="paramName">The name of the parameter being checked.</param>
    public static void ThrowIfArgumentIsNull<T>([ValidatedNotNull][NoEnumeration] T obj, [CallerArgumentExpression(nameof(obj))] string paramName = "")
    {
        if (obj is null)
        {
            throw new ArgumentNullException(paramName);
        }
    }

    /// <summary>
    /// Throws an <see cref="ArgumentNullException"/> with a custom message if the argument is null.
    /// </summary>
    /// <typeparam name="T">The type of the object being checked.</typeparam>
    /// <param name="obj">The object to check.</param>
    /// <param name="paramName">The name of the parameter being checked.</param>
    /// <param name="message">The custom error message.</param>
    public static void ThrowIfArgumentIsNull<T>([ValidatedNotNull][NoEnumeration] T obj, string paramName, string message)
    {
        if (obj is null)
        {
            throw new ArgumentNullException(paramName, message);
        }
    }

    /// <summary>
    /// Throws an exception if a string argument is null or empty.
    /// </summary>
    /// <param name="str">The string to check.</param>
    /// <param name="paramName">The name of the parameter being checked.</param>
    public static void ThrowIfArgumentIsNullOrEmpty([ValidatedNotNull] string str, [CallerArgumentExpression(nameof(str))] string paramName = "")
    {
        if (string.IsNullOrEmpty(str))
        {
            throw new ArgumentException("The value cannot be null or an empty string.", paramName);
        }
    }

    /// <summary>
    /// Throws an exception if a string argument is null, empty, or whitespace.
    /// </summary>
    /// <param name="str">The string to check.</param>
    /// <param name="paramName">The name of the parameter being checked.</param>
    public static void ThrowIfArgumentIsEmptyOrWhiteSpace([ValidatedNotNull] string str, [CallerArgumentExpression(nameof(str))] string paramName = "")
    {
        if (string.IsNullOrWhiteSpace(str))
        {
            throw new ArgumentException("The value cannot be null, empty, or whitespace.", paramName);
        }
    }

    /// <summary>
    /// Throws an exception if a value is not within a specified numeric range.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="minValue">The minimum allowable value.</param>
    /// <param name="maxValue">The maximum allowable value.</param>
    /// <param name="paramName">The name of the parameter being checked.</param>
    public static void ThrowIfArgumentIsNotInRange(int value, int minValue, int maxValue, [CallerArgumentExpression(nameof(value))] string paramName = "")
    {
        if (value < minValue || value > maxValue)
        {
            throw new ArgumentOutOfRangeException(paramName, $"Value must be between {minValue} and {maxValue}. Actual value: {value}");
        }
    }

    /// <summary>
    /// Throws an exception if a collection contains a null value.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="values">The collection to check.</param>
    /// <param name="paramName">The name of the parameter being checked.</param>
    public static void ThrowIfArgumentContainsNull<T>(IEnumerable<T> values, [CallerArgumentExpression(nameof(values))] string paramName = "")
    {
        if (values.Any(t => t is null))
        {
            throw new ArgumentNullException(paramName, "Collection contains a null value");
        }
    }

    /// <summary>
    /// Throws an exception if the argument is not defined in the specified enum type.
    /// </summary>
    /// <typeparam name="T">The enum type.</typeparam>
    /// <param name="value">The enum value to check.</param>
    /// <param name="paramName">The name of the parameter being checked.</param>
    public static void ThrowIfArgumentIsOutOfRange<T>(T value, [CallerArgumentExpression(nameof(value))] string paramName = "")
        where T : Enum
    {
        if (!Enum.IsDefined(typeof(T), value))
        {
            throw new ArgumentOutOfRangeException(paramName, $"Value '{value}' is not defined in the enum {typeof(T).Name}.");
        }
    }

    /// <summary>
    /// Throws an exception if a string does not match a specified pattern.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="pattern">The regular expression pattern to match.</param>
    /// <param name="paramName">The name of the parameter being checked.</param>
    public static void ThrowIfArgumentDoesNotMatchPattern(string value, string pattern, [CallerArgumentExpression(nameof(value))] string paramName = "")
    {
        if (!Regex.IsMatch(value, pattern))
        {
            throw new ArgumentException($"The value does not match the expected pattern: {pattern}.", paramName);
        }
    }

    /// <summary>
    /// Throws an exception if a numeric argument is negative or zero.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="paramName">The name of the parameter being checked.</param>
    public static void ThrowIfArgumentIsNegativeOrZero(int value, [CallerArgumentExpression(nameof(value))] string paramName = "")
    {
        if (value <= 0)
        {
            throw new ArgumentOutOfRangeException(paramName, "The value must be greater than zero.");
        }
    }
    /// <summary>
    /// Throws an exception if a string argument is null or empty.
    /// </summary>
    /// <param name="str">The string to check.</param>
    /// <param name="paramName">The name of the parameter being checked.</param>
    /// <param name="message">The custom error message to use in the exception.</param>
    public static void ThrowIfArgumentIsNullOrEmpty([ValidatedNotNull] string str, string paramName, string message)
    {
        if (string.IsNullOrEmpty(str))
        {
            ThrowIfArgumentIsNull(str, paramName, message);
            throw new ArgumentException(message, paramName);
        }
    }

    /// <summary>
    /// Throws an exception if a collection is empty.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="values">The collection to check.</param>
    /// <param name="paramName">The name of the parameter being checked.</param>
    /// <param name="message">The custom error message to use in the exception.</param>
    public static void ThrowIfArgumentIsEmpty<T>(IEnumerable<T> values, string paramName, string message)
    {
        if (!values.Any())
        {
            throw new ArgumentException(message, paramName);
        }
    }

    /// <summary>
    /// Throws an exception if a string argument is empty.
    /// </summary>
    /// <param name="str">The string to check.</param>
    /// <param name="paramName">The name of the parameter being checked.</param>
    /// <param name="message">The custom error message to use in the exception.</param>
    public static void ThrowIfArgumentIsEmpty(string str, string paramName, string message)
    {
        if (str.Length == 0)
        {
            throw new ArgumentException(message, paramName);
        }
    }

    /// <summary>
    /// Throws an exception if a <see cref="TimeSpan"/> value is negative.
    /// </summary>
    /// <param name="timeSpan">The value to check.</param>
    /// <param name="paramName">The name of the parameter being checked.</param>
    public static void ThrowIfArgumentIsNegative(TimeSpan timeSpan, [CallerArgumentExpression(nameof(timeSpan))] string paramName = "")
    {
        if (timeSpan < TimeSpan.Zero)
        {
            throw new ArgumentOutOfRangeException(paramName, "The value must be non-negative.");
        }
    }

    /// <summary>
    /// Throws an exception if a float value is negative.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="paramName">The name of the parameter being checked.</param>
    public static void ThrowIfArgumentIsNegative(float value, [CallerArgumentExpression(nameof(value))] string paramName = "")
    {
        if (value < 0)
        {
            throw new ArgumentOutOfRangeException(paramName, "The value must be non-negative.");
        }
    }

    /// <summary>
    /// Throws an exception if a double value is negative.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="paramName">The name of the parameter being checked.</param>
    public static void ThrowIfArgumentIsNegative(double value, [CallerArgumentExpression(nameof(value))] string paramName = "")
    {
        if (value < 0)
        {
            throw new ArgumentOutOfRangeException(paramName, "The value must be non-negative.");
        }
    }

    /// <summary>
    /// Throws an exception if a decimal value is negative.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="paramName">The name of the parameter being checked.</param>
    public static void ThrowIfArgumentIsNegative(decimal value, [CallerArgumentExpression(nameof(value))] string paramName = "")
    {
        if (value < 0)
        {
            throw new ArgumentOutOfRangeException(paramName, "The value must be non-negative.");
        }
    }

    /// <summary>
    /// Throws an exception if a numeric value is not within the specified range.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="minValue">The minimum allowable value.</param>
    /// <param name="maxValue">The maximum allowable value.</param>
    /// <param name="paramName">The name of the parameter being checked.</param>
    public static void ThrowIfArgumentIsNotInRange(double value, double minValue, double maxValue, [CallerArgumentExpression(nameof(value))] string paramName = "")
    {
        if (value < minValue || value > maxValue)
        {
            throw new ArgumentOutOfRangeException(paramName, $"Value must be between {minValue} and {maxValue}. Actual value: {value}");
        }
    }

    /// <summary>
    /// Throws an exception if a decimal value is zero or negative.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="paramName">The name of the parameter being checked.</param>
    public static void ThrowIfArgumentIsNegativeOrZero(decimal value, [CallerArgumentExpression(nameof(value))] string paramName = "")
    {
        if (value <= 0)
        {
            throw new ArgumentOutOfRangeException(paramName, "The value must be greater than zero.");
        }
    }

    /// <summary>
    /// Throws an exception if an enum value is not defined in the enum type.
    /// </summary>
    /// <typeparam name="T">The enum type.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="paramName">The name of the parameter being checked.</param>
    public static void ThrowIfArgumentIsNotInEnumRange<T>(T value, [CallerArgumentExpression(nameof(value))] string paramName = "") where T : Enum
    {
        if (!Enum.IsDefined(typeof(T), value))
        {
            throw new ArgumentOutOfRangeException(paramName, $"Value '{value}' is not defined in the enum {typeof(T).Name}.");
        }
    }

    /// <summary>
    /// Throws an exception if a <see cref="DateTime"/> value is not within the specified range.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="minValue">The minimum allowable value.</param>
    /// <param name="maxValue">The maximum allowable value.</param>
    /// <param name="paramName">The name of the parameter being checked.</param>
    public static void ThrowIfArgumentIsNotInRange(DateTime value, DateTime minValue, DateTime maxValue, [CallerArgumentExpression(nameof(value))] string paramName = "")
    {
        if (value < minValue || value > maxValue)
        {
            throw new ArgumentOutOfRangeException(paramName, $"Value must be between {minValue} and {maxValue}. Actual value: {value}");
        }
    }

    /// <summary>
    /// Throws an exception if a <see cref="Guid"/> value is not within the specified range.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="minValue">The minimum allowable value.</param>
    /// <param name="maxValue">The maximum allowable value.</param>
    /// <param name="paramName">The name of the parameter being checked.</param>
    public static void ThrowIfArgumentIsNotInRange(Guid value, Guid minValue, Guid maxValue, [CallerArgumentExpression(nameof(value))] string paramName = "")
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

