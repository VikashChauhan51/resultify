namespace ResultifyCore;
public enum OutcomeStatus : byte
{
    Success = 1,
    Failure = 2,
    Validation = 3,
    Problem = 4,
    NotFound = 5,
    Conflict = 6,
    Unauthorized = 7
}
