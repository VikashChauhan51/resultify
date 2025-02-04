using System.Runtime.CompilerServices;

namespace ResultifyCore;

/// <summary>
/// Provides methods to guard against invalid conditions.
/// </summary>
/// <exception cref="ArgumentNullException">Thrown when a null value is encountered.</exception>
/// <exception cref="ArgumentException">Thrown when an argument is invalid.</exception>
/// <exception cref="ArgumentOutOfRangeException">Thrown when an argument is out of range.</exception>
/// <exception cref="KeyNotFoundException">Thrown when a key is not found.</exception>
public interface IGuardClause
{
    /// <summary>
    /// Ensures the specified value is not null.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <returns>The current guard clause instance.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the value is null.</exception>
    IGuardClause Null(object? value, string paramName);
    /// <summary>
    /// Ensures the specified string is not null or empty.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <returns>The current guard clause instance.</returns>
    /// <exception cref="ArgumentException">Thrown when the string is null or empty.</exception>
    IGuardClause NullOrEmpty(string? value, string paramName);
    /// <summary>
    /// Ensures the specified string is not null or whitespace.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <returns>The current guard clause instance.</returns>
    /// <exception cref="ArgumentException">Thrown when the string is null or whitespace.</exception>
    IGuardClause NullOrWhiteSpace(string? value, string paramName);
    /// <summary>
    /// Ensures the specified value is within the specified range.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="min">The minimum allowed value.</param>
    /// <param name="max">The maximum allowed value.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <returns>The current guard clause instance.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is out of range.</exception>
    IGuardClause OutOfRange<T>(T value, T min, T max, string paramName) where T : IComparable<T>;
    /// <summary>
    /// Ensures the specified value is not less than the specified minimum.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="min">The minimum allowed value.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <returns>The current guard clause instance.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is less than the minimum.</exception>
    IGuardClause LessThan<T>(T value, T min, string paramName) where T : IComparable<T>;
    /// <summary>
    /// Ensures the specified value is not greater than the specified maximum.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="max">The maximum allowed value.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <returns>The current guard clause instance.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is greater than the maximum.</exception>
    IGuardClause GreaterThan<T>(T value, T max, string paramName) where T : IComparable<T>;
    /// <summary>
    /// Ensures the specified integer value is not negative or zero.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <returns>The current guard clause instance.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is negative or zero.</exception>
    IGuardClause NegativeOrZero(int value, string paramName);
    /// <summary>
    /// Ensures the specified double value is not negative or zero.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <returns>The current guard clause instance.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is negative or zero.</exception>
    IGuardClause NegativeOrZero(double value, string paramName);
    /// <summary>
    /// Ensures the specified value is not equal to the expected value.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="expected">The expected value.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <returns>The current guard clause instance.</returns>
    /// <exception cref="ArgumentException">Thrown when the value is equal to the expected value.</exception>
    IGuardClause NotEqual<T>(T value, T expected, string paramName);
    /// <summary>
    /// Ensures the specified condition is not true.
    /// </summary>
    /// <param name="condition">The condition to check.</param>
    /// <param name="message">The message to include in the exception if the condition is true.</param>
    /// <returns>The current guard clause instance.</returns>
    /// <exception cref="ArgumentException">Thrown when the condition is true.</exception>
    IGuardClause InvalidCondition(bool condition, string message);
    /// <summary>
    /// Ensures the specified string matches the specified pattern.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="pattern">The pattern to match.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <returns>The current guard clause instance.</returns>
    /// <exception cref="ArgumentException">Thrown when the string does not match the pattern.</exception>
    IGuardClause InvalidFormat(string value, string pattern, string paramName);
    /// <summary>
    /// Ensures the specified email is valid.
    /// </summary>
    /// <param name="email">The email to check.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <returns>The current guard clause instance.</returns>
    /// <exception cref="ArgumentException">Thrown when the email is invalid.</exception>
    IGuardClause InvalidEmail(string email, string paramName);
    /// <summary>
    /// Ensures the specified collection is not null or empty.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the collection.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <returns>The current guard clause instance.</returns>
    /// <exception cref="ArgumentException">Thrown when the collection is null or empty.</exception>
    IGuardClause NullOrEmptyCollection<T>(IEnumerable<T>? collection, string paramName);
    /// <summary>
    /// Ensures the specified value is not null.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <paramName="paramName">The name of the parameter.</paramName>
    /// <returns>The current guard clause instance.</returns>
    /// <exception cref="KeyNotFoundException">Thrown when the value is not found.</exception>
    IGuardClause NotFound<T>(T? value, string paramName);
    /// <summary>
    /// Ensures the specified date range is valid.
    /// </summary>
    /// <param name="startDate">The start date.</param>
    /// <param name="endDate">The end date.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <returns>The current guard clause instance.</returns>
    /// <exception cref="ArgumentException">Thrown when the start date is after the end date.</exception>
    IGuardClause InvalidDateRange(DateTime startDate, DateTime endDate, string paramName);
    /// <summary>
    /// Ensures the specified date is not in the past.
    /// </summary>
    /// <param name="date">The date to check.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <returns>The current guard clause instance.</returns>
    /// <exception cref="ArgumentException">Thrown when the date is in the past.</exception>
    IGuardClause PastDate(DateTime date, string paramName);
    /// <summary>
    /// Ensures the specified date is not in the future.
    /// </summary>
    /// <param name="date">The date to check.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <returns>The current guard clause instance.</returns>
    /// <exception cref="ArgumentException">Thrown when the date is in the future.</exception>
    IGuardClause FutureDate(DateTime date, string paramName);
    /// <summary>
    /// Ensures the specified enum value is valid.
    /// </summary>
    /// <typeparam name="TEnum">The type of the enum.</typeparam>
    /// <param name="value">The enum value to check.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <returns>The current guard clause instance.</returns>
    /// <exception cref="ArgumentException">Thrown when the enum value is invalid.</exception>
    IGuardClause InvalidEnum<TEnum>(TEnum value, string paramName) where TEnum : struct, Enum;
    /// <summary>
    /// Ensures the specified value is not null or default.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <returns>The current guard clause instance.</returns>
    /// <exception cref="ArgumentException">Thrown when the value is null or default.</exception>
    IGuardClause NullOrDefault<T>(T value, string paramName);
    /// <summary>
    /// Ensures the specified value satisfies the specified predicate.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="predicate">The predicate to satisfy.</param>
    /// <param name="message">The message to include in the exception if the predicate is not satisfied.</param>
    /// <returns>The current guard clause instance.</returns>
    /// <exception cref="ArgumentException">Thrown when the predicate is not satisfied.</exception>
    IGuardClause InvalidPredicate<T>(T value, Func<T, bool> predicate, string message);


    /// <summary>
    /// Throws an <see cref="InvalidOperationException"/> if the provided object is of the specified type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type to check against.</typeparam>
    /// <param name="obj">The object to validate.</param>
    /// <param name="paramName">The name of the parameter (automatically inferred).</param>
    /// <exception cref="InvalidOperationException">Thrown if the object is of type <typeparamref name="T"/>.</exception>
    IGuardClause ArgumentTypeMatch<T>(object obj, [CallerArgumentExpression(nameof(obj))] string paramName = "");

    /// <summary>
    /// Throws an <see cref="InvalidOperationException"/> if the provided object is not of the specified type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type to check against.</typeparam>
    /// <param name="obj">The object to validate.</param>
    /// <param name="paramName">The name of the parameter (automatically inferred).</param>
    /// <exception cref="InvalidOperationException">Thrown if the object is not of type <typeparamref name="T"/>.</exception>
    IGuardClause ArgumentTypeNotMatch<T>(object obj, [CallerArgumentExpression(nameof(obj))] string paramName = "");

    /// <summary>
    /// Throws an <see cref="ArgumentException"/> if the provided string contains potentially dangerous scripts (XSS).
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="paramName">The name of the parameter (automatically inferred).</param>
    /// <exception cref="ArgumentException">Thrown if the string contains HTML or JavaScript-like content.</exception>
    IGuardClause ArgumentContainsScripts(string value, [CallerArgumentExpression(nameof(value))] string paramName = "");

    /// <summary>
    /// Throws an <see cref="ArgumentException"/> if the provided string contains potential SQL injection characters.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="paramName">The name of the parameter (automatically inferred).</param>
    /// <exception cref="ArgumentException">Thrown if the string contains SQL injection characters (e.g., quotes).</exception>
    IGuardClause ArgumentContainsInjection(string value, [CallerArgumentExpression(nameof(value))] string paramName = "");


}
