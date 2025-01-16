namespace ResultifyCore.Tests;

[Trait("Category", "Unit")]
public class OneOfTests
{
    [Fact]
    public void OneOf_ShouldStoreValueOfTypeT2()
    {
        // Arrange
        var expectedValue = 42;
        var oneOf = new OneOf<string, int, double, char, bool, DateTime, Guid>(expectedValue);

        // Act
        var isT2 = oneOf.IsT2;
        var value = oneOf.AsT2;

        // Assert
        Assert.True(isT2);
        Assert.Equal(expectedValue, value);
    }

    [Fact]
    public void OneOf_ShouldStoreValueOfTypeT3()
    {
        // Arrange
        var expectedValue = 42.5;
        var oneOf = new OneOf<string, int, double, char, bool, DateTime, Guid>(expectedValue);

        // Act
        var isT3 = oneOf.IsT3;
        var value = oneOf.AsT3;

        // Assert
        Assert.True(isT3);
        Assert.Equal(expectedValue, value);
    }

    [Fact]
    public void OneOf_ShouldStoreValueOfTypeT4()
    {
        // Arrange
        var expectedValue = 'X';
        var oneOf = new OneOf<string, int, double, char, bool, DateTime, Guid>(expectedValue);

        // Act
        var isT4 = oneOf.IsT4;
        var value = oneOf.AsT4;

        // Assert
        Assert.True(isT4);
        Assert.Equal(expectedValue, value);
    }

    [Fact]
    public void OneOf_ShouldStoreValueOfTypeT5()
    {
        // Arrange
        var expectedValue = true;
        var oneOf = new OneOf<string, int, double, char, bool, DateTime, Guid>(expectedValue);

        // Act
        var isT5 = oneOf.IsT5;
        var value = oneOf.AsT5;

        // Assert
        Assert.True(isT5);
        Assert.Equal(expectedValue, value);
    }

    [Fact]
    public void OneOf_ShouldStoreValueOfTypeT6()
    {
        // Arrange
        var expectedValue = new DateTime(2025, 1, 1);
        var oneOf = new OneOf<string, int, double, char, bool, DateTime, Guid>(expectedValue);

        // Act
        var isT6 = oneOf.IsT6;
        var value = oneOf.AsT6;

        // Assert
        Assert.True(isT6);
        Assert.Equal(expectedValue, value);
    }

    [Fact]
    public void OneOf_ShouldStoreValueOfTypeT7()
    {
        // Arrange
        var expectedValue = Guid.NewGuid();
        var oneOf = new OneOf<string, int, double, char, bool, DateTime, Guid>(expectedValue);

        // Act
        var isT7 = oneOf.IsT7;
        var value = oneOf.AsT7;

        // Assert
        Assert.True(isT7);
        Assert.Equal(expectedValue, value);
    }

    [Fact]
    public void OneOf_ShouldThrowInvalidOperationException_WhenAccessingWrongType()
    {
        // Arrange
        var oneOf = new OneOf<string, int, double, char, bool, DateTime, Guid>('X');

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => oneOf.AsT1);
        Assert.Throws<InvalidOperationException>(() => oneOf.AsT2);
        Assert.Throws<InvalidOperationException>(() => oneOf.AsT3);
        Assert.Throws<InvalidOperationException>(() => oneOf.AsT5);
        Assert.Throws<InvalidOperationException>(() => oneOf.AsT6);
        Assert.Throws<InvalidOperationException>(() => oneOf.AsT7);
    }

    [Fact]
    public void OneOf_Match_ShouldReturnCorrectResultBasedOnType()
    {
        // Arrange
        var oneOf = new OneOf<string, int, double, char, bool, DateTime, Guid>(42);

        // Act
        var result = oneOf.Match(
            t1 => "String",
            t2 => $"Integer: {t2}",
            t3 => "Double",
            t4 => "Char",
            t5 => "Bool",
            t6 => "DateTime",
            t7 => "Guid"
        );

        // Assert
        Assert.Equal("Integer: 42", result);
    }

    [Fact]
    public void OneOf_Equality_ShouldReturnTrueForEqualInstances()
    {
        // Arrange
        var oneOf1 = new OneOf<string, int, double, char, bool, DateTime, Guid>(42);
        var oneOf2 = new OneOf<string, int, double, char, bool, DateTime, Guid>(42);

        // Act & Assert
        Assert.True(oneOf1 == oneOf2);
        Assert.False(oneOf1 != oneOf2);
    }

    [Fact]
    public void OneOf_Equality_ShouldReturnFalseForDifferentInstances()
    {
        // Arrange
        var oneOf1 = new OneOf<string, int, double, char, bool, DateTime, Guid>(42);
        var oneOf2 = new OneOf<string, int, double, char, bool, DateTime, Guid>(43);

        // Act & Assert
        Assert.False(oneOf1 == oneOf2);
        Assert.True(oneOf1 != oneOf2);
    }

    [Fact]
    public void OneOf_Comparison_ShouldReturnCorrectResult()
    {
        // Arrange
        var oneOf1 = new OneOf<string, int, double, char, bool, DateTime, Guid>(42);
        var oneOf2 = new OneOf<string, int, double, char, bool, DateTime, Guid>(43);

        // Act & Assert
        Assert.True(oneOf1 < oneOf2);
        Assert.True(oneOf2 > oneOf1);
    }
}
