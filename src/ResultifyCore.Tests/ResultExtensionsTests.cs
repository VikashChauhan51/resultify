﻿namespace ResultifyCore.Tests;

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
            executed = r.IsSuccess;
        });

        Assert.True(executed);
        Assert.Equal(result, returnedResult);
    }

    [Fact]
    public void Map_TransformsSuccessfulResultValue()
    {
        var result = Result<int>.Success(42);

        var mappedResult = result.Map(x => x.ToString());

        Assert.True(mappedResult.IsSuccess);
        Assert.Equal("42", mappedResult.Value);
        Assert.Null(mappedResult.Exception);
    }

    [Fact]
    public void Map_ReturnsFailureResult_WhenOriginalResultIsFailed()
    {
        var exception = new InvalidOperationException("Test error");
        var result = Result<int>.Failure(exception);

        var mappedResult = result.Map(x => x.ToString());

        Assert.False(mappedResult.IsSuccess);
        Assert.Equal(exception, mappedResult.Exception);
        Assert.Null(mappedResult.Value);
    }

    [Fact]
    public void Bind_ChainsSuccessfulResults()
    {
        var result = Result<int>.Success(42);

        var chainedResult = result.Bind(x => Result<string>.Success($"Value: {x}"));

        Assert.True(chainedResult.IsSuccess);
        Assert.Equal("Value: 42", chainedResult.Value);
        Assert.Null(chainedResult.Exception);
    }

    [Fact]
    public void Bind_ReturnsOriginalFailureResult_WhenFirstResultIsFailed()
    {
        var exception = new InvalidOperationException("Test error");
        var result = Result<int>.Failure(exception);

        var chainedResult = result.Bind(x => Result<string>.Success($"Value: {x}"));

        Assert.False(chainedResult.IsSuccess);
        Assert.Equal(exception, chainedResult.Exception);
        Assert.Null(chainedResult.Value);
    }

    [Fact]
    public void Bind_ReturnsFailureResult_WhenChainedResultIsFailed()
    {
        var result = Result<int>.Success(42);
        var chainedException = new InvalidOperationException("Chained error");

        var chainedResult = result.Bind(x => Result<string>.Failure(chainedException));

        Assert.False(chainedResult.IsSuccess);
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
        var result = Result<int>.Failure(exception);
        var tappedValue = 0;

        var returnedResult = result.Tap(x =>
        {
            tappedValue = x;
        });

        Assert.Equal(0, tappedValue);
        Assert.Equal(result, returnedResult);
    }
}
