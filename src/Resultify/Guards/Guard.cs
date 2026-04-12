namespace ResultifyCore;

/// <summary>
/// The Guard class provides a fluent interface for performing guard checks against various conditions. It serves as an entry point for validating method arguments, ensuring that they meet specific criteria before proceeding with the execution of the method. The Against property returns an instance of IGuardClause, which contains methods for performing common guard checks such as null checks, empty string checks, range checks, and more. This design promotes clean and readable code by allowing developers to express validation logic in a fluent and intuitive manner.
/// </summary>
public static class Guard
{
    /// <summary>
    /// The Against property provides access to an instance of IGuardClause, which contains methods for performing various guard checks. This allows developers to write fluent validation code, such as Guard.Against.Null(argumentName) or Guard.Against.EmptyString(argumentName), to ensure that method arguments meet the required conditions before proceeding with the execution of the method.
    /// </summary>
    public static IGuardClause Against => new GuardClause();
}

