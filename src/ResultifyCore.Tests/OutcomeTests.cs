﻿using AutoBogus;


namespace ResultifyCore.Tests;

[Trait("Category", "Unit")]
public class OutcomeTests
{
    [Fact]
    public void Success_ShouldCreateSuccessfulOutcome()
    {
        // Act
        var outcome = Outcome.Success();

        // Assert
        Assert.True(outcome.Status == ResultState.Success);
        Assert.Empty(outcome.Errors);
    }

    [Fact]
    public void Failure_WithErrors_ShouldCreateFailedOutcome()
    {
        // Arrange
        var errors = AutoFaker.Generate<OutcomeError>(3).ToArray();

        // Act
        var outcome = Outcome.Failure(errors);

        // Assert
        Assert.True(outcome.Status != ResultState.Success);
        Assert.NotEmpty(outcome.Errors);
        Assert.Equivalent(errors, outcome.Errors);
    }


    [Fact]
    public void Match_ShouldInvokeCorrectCallback()
    {
        // Arrange
        var successOutcome = Outcome.Success();
        var failureOutcome = Outcome.Failure(new OutcomeError("E001", "Test error"));

        // Act & Assert
        successOutcome.Match(
            onSuccess: () => { /* No exception expected */ },
            onFailure: (status, errors) => throw new InvalidOperationException("Should not call onFailure"));

        failureOutcome.Match(
            onSuccess: () => throw new InvalidOperationException("Should not call onSuccess"),
            onFailure: (status, errors) => Assert.True(errors.Any(e => e.Code == "E001")));
    }

    [Fact]
    public void Equals_ShouldReturnTrueForEqualOutcomes()
    {
        // Arrange
        var outcome1 = Outcome.Failure(new OutcomeError("E001", "Error 1"));
        var outcome2 = Outcome.Failure(new OutcomeError("E001", "Error 1"));

        // Act
        var isEqual = outcome1.Equals(outcome2);

        // Assert
        Assert.True(isEqual);
    }
}
