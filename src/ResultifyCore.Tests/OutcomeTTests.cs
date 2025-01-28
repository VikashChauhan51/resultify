using AutoBogus;

namespace ResultifyCore.Tests;

[Trait("Category", "Unit")]
public class OutcomeTTests
{
    [Fact]
    public void Success_ShouldCreateSuccessfulOutcomeWithValue()
    {
        // Arrange
        var value = AutoFaker.Generate<string>();

        // Act
        var outcome = Outcome<string>.Success(value);

        // Assert
        Assert.True(outcome.Status == OutcomeStatus.Success);
        Assert.Equal(value, outcome.Value);
        Assert.Empty(outcome.Errors);
    }

    [Fact]
    public void Success_WithNullValue_ShouldThrowArgumentNullException()
    {
        // Act
        Action act = () => Outcome<string>.Success(null);

        // Assert
        Assert.Throws<ArgumentNullException>(act);
    }

    [Fact]
    public void Failure_WithErrors_ShouldCreateFailedOutcome()
    {
        // Arrange
        var errors = AutoFaker.Generate<OutcomeError>(3).ToArray();

        // Act
        var outcome = Outcome<string>.Failure(errors);

        // Assert
        Assert.True(outcome.Status!=OutcomeStatus.Success);
        Assert.Equivalent(errors, outcome.Errors);
        Assert.Null(outcome.Value);
    }

    [Fact]
    public void Unwrap_ShouldReturnValueForSuccessfulOutcome()
    {
        // Arrange
        var value = AutoFaker.Generate<string>();
        var outcome = Outcome<string>.Success(value);

        // Act
        var unwrappedValue = outcome.Unwrap();

        // Assert
        Assert.Equal(value, unwrappedValue);
    }

    [Fact]
    public void Unwrap_ShouldThrowInvalidOperationExceptionForFailedOutcome()
    {
        // Arrange
        var outcome = Outcome<string>.Failure(new OutcomeError("E001", "Test error"));

        // Act
        Action act = () => outcome.Unwrap();

        // Assert
        Assert.Throws<InvalidOperationException>(act);
    }
}
