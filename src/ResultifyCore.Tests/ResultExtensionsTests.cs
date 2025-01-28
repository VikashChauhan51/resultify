namespace ResultifyCore.Tests;

[Trait("Category", "Unit")]
public class ResultExtensionsTests
{
    [Fact]
    public void Do_ExecutesActionAndReturnsOriginalResult()
    {
        var result = Result<int>.Success(42);
        var executed = false;

        var returnedResult = result.Do(r =>
        {
            executed = r.Status == ResultState.Success;
        });

        Assert.True(executed);
        Assert.Equal(result, returnedResult);
    }

    [Fact]
    public void Map_TransformsSuccessfulResultValue()
    {
        var result = Result<int>.Success(42);

        var mappedResult = result.Map(x => x.ToString());

        Assert.True(mappedResult.Status == ResultState.Success);
        Assert.Equal("42", mappedResult.Value);
        Assert.Null(mappedResult.Exception);
    }

    [Fact]
    public void Map_ReturnsFailureResult_WhenOriginalResultIsFailed()
    {
        var exception = new InvalidOperationException("Test error");
        var result = Result<int>.Failure(ResultState.Failure, exception);

        var mappedResult = result.Map(x => x.ToString());

        Assert.True(mappedResult.Status != ResultState.Success);
        Assert.Equal(exception, mappedResult.Exception);
        Assert.Null(mappedResult.Value);
    }

    [Fact]
    public void Bind_ChainsSuccessfulResults()
    {
        var result = Result<int>.Success(42);

        var chainedResult = result.Bind(x => Result<string>.Success($"Value: {x}"));

        Assert.True(chainedResult.Status == ResultState.Success);
        Assert.Equal("Value: 42", chainedResult.Value);
        Assert.Null(chainedResult.Exception);
    }

    [Fact]
    public void Bind_ReturnsOriginalFailureResult_WhenFirstResultIsFailed()
    {
        var exception = new InvalidOperationException("Test error");
        var result = Result<int>.Failure(ResultState.Failure,exception);

        var chainedResult = result.Bind(x => Result<string>.Success($"Value: {x}"));

        Assert.True(chainedResult.Status != ResultState.Success);
        Assert.Equal(exception, chainedResult.Exception);
        Assert.Null(chainedResult.Value);
    }

    [Fact]
    public void Bind_ReturnsFailureResult_WhenChainedResultIsFailed()
    {
        var result = Result<int>.Success(42);
        var chainedException = new InvalidOperationException("Chained error");

        var chainedResult = result.Bind(x => Result<string>.Failure(ResultState.Failure, chainedException));

        Assert.False(chainedResult.Status == ResultState.Success);
        Assert.Equal(chainedException, chainedResult.Exception);
        Assert.Null(chainedResult.Value);
    }

    [Fact]
    public void Tap_ExecutesActionForSuccessfulResult()
    {
        var result = Result<int>.Success(42);
        var tappedValue = 0;

        var returnedResult = result.Tap(x =>
        {
            tappedValue = x;
        });

        Assert.Equal(42, tappedValue);
        Assert.Equal(result, returnedResult);
    }

    [Fact]
    public void Tap_DoesNotExecuteActionForFailureResult()
    {
        var exception = new InvalidOperationException("Test error");
        var result = Result<int>.Failure(ResultState.Failure, exception);
        var tappedValue = 0;

        var returnedResult = result.Tap(x =>
        {
            tappedValue = x;
        });

        Assert.Equal(0, tappedValue);
        Assert.Equal(result, returnedResult);
    }

    [Fact]
    public void Success_ShouldReturnResult_WhenValueIsNotException()
    {
        // Arrange
        var value = "Test Value";

        // Act
        var result = value.Success();

        // Assert
        Assert.True(result.Status == ResultState.Success); // Ensure it's a success result
        Assert.Equal(value, result.Value); // Ensure the value is correctly set
    }

    [Fact]
    public void Success_ShouldThrowInvalidOperationException_WhenValueIsException()
    {
        // Arrange
        var value = new Exception("Test Exception");

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => value.Success());
        Assert.Equal("Cannot use an Exception as a value.", exception.Message);
    }

    [Fact]
    public void Failure_ShouldReturnResult_WhenExceptionIsValid()
    {
        // Arrange
        var exception = new Exception("Test Exception");

        // Act
        var result = exception.Failure<string>(); // Testing with a generic parameter

        // Assert
        Assert.True(result.Status != ResultState.Success); // Ensure it's a failure result
        Assert.Equal(exception, result.Exception); // Ensure the exception is correctly set
    }

    [Fact]
    public void Failure_ShouldThrowArgumentNullException_WhenExceptionIsNull()
    {
        // Arrange
        Exception exception = null;

        // Act & Assert
        var ex = Assert.Throws<ArgumentNullException>(() => exception.Failure<string>());
        Assert.Equal("exception", ex.ParamName); // Ensure the exception is for the correct parameter
    }

    [Fact]
    public void Failure_ShouldReturnResult_WhenExceptionIsValid_ForNonGenericResult()
    {
        // Arrange
        var exception = new Exception("Test Exception");

        // Act
        var result = exception.Failure();

        // Assert
        Assert.True(result.Status != ResultState.Success); // Ensure it's a failure result
        Assert.Equal(exception, result.Exception); // Ensure the exception is correctly set
    }

    [Fact]
    public void Failure_ShouldThrowArgumentNullException_WhenExceptionIsNull_ForNonGenericResult()
    {
        // Arrange
        Exception exception = null;

        // Act & Assert
        var ex = Assert.Throws<ArgumentNullException>(() => exception.Failure());
        Assert.Equal("exception", ex.ParamName); // Ensure the exception is for the correct parameter
    }


}

