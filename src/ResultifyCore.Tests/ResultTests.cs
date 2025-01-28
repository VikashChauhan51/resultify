namespace ResultifyCore.Tests;

[Trait("Category", "Unit")]
public class ResultTests
{
    [Fact]
    public void Success_CreatesSuccessfulResult()
    {
        var result = Result<int>.Success(42);
        Assert.True(result.Status == ResultState.Success);
        Assert.Equal(42, result.Value);
        Assert.Null(result.Exception);
    }

    [Fact]
    public void Failure_CreatesFailedResult()
    {
        var exception = new InvalidOperationException("Test error");
        var result = Result<int>.Failure(ResultState.Failure, exception);

        Assert.True(result.Status != ResultState.Success);
        Assert.Equal(exception, result.Exception);
        Assert.Equal(default(int),result.Value);
    }

    [Fact]
    public void ImplicitConversion_FromValue_CreatesSuccessfulResult()
    {
        Result<int> result = 42;

        Assert.True(result.Status == ResultState.Success);
        Assert.Equal(42, result.Value);
        Assert.Null(result.Exception);
    }

    [Fact]
    public void ImplicitConversion_FromException_CreatesFailedResult()
    {
        var exception = new InvalidOperationException("Test error");
        Result<int> result = exception;

        Assert.True(result.Status != ResultState.Success);
        Assert.Equal(exception, result.Exception);
        Assert.Equal(default(int),result.Value);
    }

    [Fact]
    public void Match_ExecutesOnSuccessFunction_WhenResultIsSuccessful()
    {
        var result = Result<int>.Success(42);
        var output = result.Match(
            value => value.ToString(),
            error => error.Message
        );

        Assert.Equal("42", output);
    }

    [Fact]
    public void Match_ExecutesOnFailureFunction_WhenResultIsFailed()
    {
        var exception = new InvalidOperationException("Test error");
        var result = Result<int>.Failure(ResultState.Failure, exception);
        var output = result.Match(
            value => value.ToString(),
            error => error.Message
        );

        Assert.Equal("Test error", output);
    }

    [Fact]
    public void Match_WithAction_ExecutesOnSuccessAction_WhenResultIsSuccessful()
    {
        var result = Result<int>.Success(42);
        var successExecuted = false;
        var failureExecuted = false;

        result.Match(
            value => successExecuted = true,
            error => failureExecuted = true
        );

        Assert.True(successExecuted);
        Assert.False(failureExecuted);
    }

    [Fact]
    public void Match_WithAction_ExecutesOnFailureAction_WhenResultIsFailed()
    {
        var exception = new InvalidOperationException("Test error");
        var result = Result<int>.Failure(ResultState.Failure, exception);
        var successExecuted = false;
        var failureExecuted = false;

        result.Match(
            value => successExecuted = false,
            error => failureExecuted = true
        );

        Assert.False(successExecuted);
        Assert.True(failureExecuted);
    }

    [Fact]
    public void OnSuccess_ExecutesAction_WhenResultIsSuccessful()
    {
        var result = Result<int>.Success(42);
        var executed = false;

        result.OnSuccess(value => executed = true);

        Assert.True(executed);
    }

    [Fact]
    public void OnFailure_ExecutesAction_WhenResultIsFailed()
    {
        var exception = new InvalidOperationException("Test error");
        var result = Result<int>.Failure(ResultState.Failure, exception);
        var executed = false;

        result.OnFailure(error => executed = true);

        Assert.True(executed);
    }

    [Fact]
    public void Unwrap_ReturnsValue_WhenResultIsSuccessful()
    {
        var result = Result<int>.Success(42);

        Assert.Equal(42, result.Unwrap());
    }

    [Fact]
    public void Unwrap_ThrowsException_WhenResultIsFailed()
    {
        var exception = new ResultFailureException();
        var result = Result<int>.Failure(ResultState.Failure, exception);

        Assert.Throws<ResultFailureException>(() => result.Unwrap());
    }

    [Fact]
    public void Equals_ReturnsTrue_ForEqualSuccessfulResults()
    {
        var result1 = Result<int>.Success(42);
        var result2 = Result<int>.Success(42);

        Assert.True(result1.Equals(result2));
    }

    [Fact]
    public void Equals_ReturnsTrue_ForEqualFailedResults()
    {
        var exception = new InvalidOperationException("Test error");
        var result1 = Result<int>.Failure(ResultState.Failure, exception);
        var result2 = Result<int>.Failure(ResultState.Failure, exception);

        Assert.True(result1.Equals(result2));
    }

    [Fact]
    public void Equals_ReturnsFalse_ForDifferentResults()
    {
        var result1 = Result<int>.Success(42);
        var result2 = Result<int>.Failure(ResultState.Failure, new InvalidOperationException("Test error"));

        Assert.False(result1.Equals(result2));
    }

    [Fact]
    public void CompareTo_ReturnsZero_ForEqualSuccessfulResults()
    {
        var result1 = Result<int>.Success(42);
        var result2 = Result<int>.Success(42);

        Assert.Equal(0, result1.CompareTo(result2));
    }

    [Fact]
    public void CompareTo_ReturnsPositive_ForGreaterSuccessfulResult()
    {
        var result1 = Result<int>.Success(43);
        var result2 = Result<int>.Success(42);

        Assert.True(result1.CompareTo(result2) > 0);
    }

    [Fact]
    public void CompareTo_ReturnsNegative_ForLesserSuccessfulResult()
    {
        var result1 = Result<int>.Success(41);
        var result2 = Result<int>.Success(42);

        Assert.True(result1.CompareTo(result2) < 0);
    }

    [Fact]
    public void CompareTo_ReturnsNegative_WhenOtherResultIsSuccessfulAndCurrentIsFailed()
    {
        var result1 = Result<int>.Failure(ResultState.Failure, new InvalidOperationException("Test error"));
        var result2 = Result<int>.Success(42);

        Assert.True(result1.CompareTo(result2) < 0);
    }

    [Fact]
    public void CompareTo_ReturnsPositive_WhenCurrentResultIsSuccessfulAndOtherIsFailed()
    {
        var result1 = Result<int>.Success(42);
        var result2 = Result<int>.Failure(ResultState.Failure, new InvalidOperationException("Test error"));

        Assert.True(result1.CompareTo(result2) > 0);
    }

    [Fact]
    public void CompareTo_ReturnsZero_ForTwoFailedResults()
    {
        var exception1 = new InvalidOperationException("Test error");
        var exception2 = new InvalidOperationException("Test error");
        var result1 = Result<int>.Failure(ResultState.Failure, exception1);
        var result2 = Result<int>.Failure(ResultState.Failure, exception2);

        Assert.Equal(0, result1.CompareTo(result2));
    }
}

