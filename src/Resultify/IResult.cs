namespace ResultifyCore;

/// <summary>
/// The IResult interface defines a standardized contract for representing the outcome of an operation, including its status and any associated errors. It provides properties to access the result's status through the Status property, which indicates whether the operation was successful, failed, or resulted in no content. The Errors property allows consumers to retrieve a dictionary of error information when the operation fails, enabling detailed error handling and reporting. This interface serves as a foundational abstraction for representing results across various operations and contexts within an application.
/// </summary>
public interface IResult
{
    /// <summary>
    /// The Status property indicates the state of the result, such as Success, Failure, or NoContent. It allows consumers to determine the outcome of an operation and handle it accordingly based on the result state.
    /// </summary>
    ResultState Status { get; }
    /// <summary>
    /// The Errors property provides a dictionary of error information when the operation fails. It allows consumers to access detailed error messages and associated data, facilitating comprehensive error handling and reporting.
    /// </summary>
    IReadOnlyDictionary<string, object> Errors { get; }

}

/// <summary>
/// The IResult<T> interface extends the IResult interface to include a Data property, which represents the value associated with a successful result. This interface allows for a more specific representation of results that include data, while still providing access to the status and error information defined in the base IResult interface. The Data property is nullable, indicating that it may not contain a value in cases where the operation did not succeed or resulted in no content. This design enables flexible handling of results across various scenarios, including those that may or may not produce a value.
/// </summary>
/// <typeparam name="T">The type of the data associated with the result.</typeparam>
public interface IResult<T> : IResult
{
    /// <summary>
    /// The Data property represents the value associated with a successful result. It is nullable, indicating that it may not contain a value in cases where the operation did not succeed or resulted in no content.
    /// </summary>
    T? Data { get; }

}
