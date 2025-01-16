namespace ResultifyCore.Tests;

[Trait("Category", "Unit")]
public class OptionTests
{
    [Fact]
    public void Some_ShouldContainValue()
    {
        // Arrange
        var value = 42;

        // Act
        var option = Option<int>.Some(value);

        // Assert
        Assert.True(option.IsSome);
        Assert.False(option.IsNone);
        Assert.Equal(value, option.Value);
    }

    [Fact]
    public void None_ShouldNotContainValue()
    {
        // Act
        var option = Option<int>.None;

        // Assert
        Assert.True(option.IsNone);
        Assert.False(option.IsSome);
        Assert.Throws<OptionNoneException>(() => option.Unwrap());
    }

    [Fact]
    public void Match_ShouldExecuteOnSomeFunction_WhenOptionIsSome()
    {
        // Arrange
        var option = Option<string>.Some("Hello");

        // Act
        var result = option.Match(
            onSome: value => $"Value is: {value}",
            onNone: () => "No value"
        );

        // Assert
        Assert.Equal("Value is: Hello", result);
    }

    [Fact]
    public void Match_ShouldExecuteOnNoneFunction_WhenOptionIsNone()
    {
        // Arrange
        var option = Option<string>.None;

        // Act
        var result = option.Match(
            onSome: value => $"Value is: {value}",
            onNone: () => "No value"
        );

        // Assert
        Assert.Equal("No value", result);
    }

    [Fact]
    public void OnSome_ShouldExecuteAction_WhenOptionIsSome()
    {
        // Arrange
        var option = Option<int>.Some(100);
        int result = 0;

        // Act
        option.OnSome(value => result = value);

        // Assert
        Assert.Equal(100, result);
    }

    [Fact]
    public void OnNone_ShouldExecuteAction_WhenOptionIsNone()
    {
        // Arrange
        var option = Option<int>.None;
        bool wasCalled = false;

        // Act
        option.OnNone(() => wasCalled = true);

        // Assert
        Assert.True(wasCalled);
    }

    [Fact]
    public void CompareTo_ShouldReturnPositive_WhenThisIsSomeAndOtherIsNone()
    {
        // Arrange
        var option1 = Option<int>.Some(5);
        var option2 = Option<int>.None;

        // Act
        var result = option1.CompareTo(option2);

        // Assert
        Assert.True(result > 0);
    }

    [Fact]
    public void CompareTo_ShouldReturnNegative_WhenThisIsNoneAndOtherIsSome()
    {
        // Arrange
        var option1 = Option<int>.None;
        var option2 = Option<int>.Some(10);

        // Act
        var result = option1.CompareTo(option2);

        // Assert
        Assert.True(result < 0);
    }

    [Fact]
    public void CompareTo_ShouldReturnZero_WhenBothAreNone()
    {
        // Arrange
        var option1 = Option<int>.None;
        var option2 = Option<int>.None;

        // Act
        var result = option1.CompareTo(option2);

        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void Equals_ShouldReturnTrue_WhenBothOptionsAreEqual()
    {
        // Arrange
        var option1 = Option<int>.Some(20);
        var option2 = Option<int>.Some(20);

        // Act
        var areEqual = option1.Equals(option2);

        // Assert
        Assert.True(areEqual);
    }

    [Fact]
    public void Equals_ShouldReturnFalse_WhenOptionsAreDifferent()
    {
        // Arrange
        var option1 = Option<int>.Some(10);
        var option2 = Option<int>.Some(20);

        // Act
        var areEqual = option1.Equals(option2);

        // Assert
        Assert.False(areEqual);
    }

    [Fact]
    public void ImplicitConversion_ShouldCreateOptionWithSomeValue()
    {
        // Arrange
        int value = 50;

        // Act
        Option<int> option = value;

        // Assert
        Assert.True(option.IsSome);
        Assert.Equal(value, option.Value);
    }

    [Fact]
    public void Unwrap_ShouldThrowException_WhenOptionIsNone()
    {
        // Arrange
        var option = Option<int>.None;

        // Act & Assert
        Assert.Throws<OptionNoneException>(() => option.Unwrap());
    }

    [Fact]
    public void Unwrap_ShouldReturnValue_WhenOptionIsSome()
    {
        // Arrange
        var option = Option<int>.Some(30);

        // Act
        var value = option.Unwrap();

        // Assert
        Assert.Equal(30, value);
    }

    [Fact]
    public void Operators_ShouldWorkCorrectly()
    {
        // Arrange
        var option1 = Option<int>.Some(5);
        var option2 = Option<int>.Some(10);
        var option3 = Option<int>.None;

        // Act & Assert
        Assert.True(option1 < option2);
        Assert.True(option2 > option1);
        Assert.True(option1 <= option2);
        Assert.True(option3 <= option1);
        Assert.True(option1 >= option3);
        Assert.True(option1 != option2);
    }
}

