namespace ResultifyCore;


/// <summary>
/// Represents the state of a result.
/// </summary>
public enum ResultState : byte
{
    Success = 1,
    Failure = 2,
    Validation = 3,
    Problem = 4,
    NotFound = 5,
    Conflict = 6,
    Unauthorized = 7
}
