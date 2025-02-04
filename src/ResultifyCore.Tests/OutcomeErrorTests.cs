namespace ResultifyCore.Tests;

[Trait("Category", "Unit")]
public class OutcomeErrorTests
{
    [Fact]
    public void Equals_ShouldReturnTrueForEqualOutcomeErrors()
    {
        // Arrange
        var error1 = new OutcomeError("E001", "Error 1");
        var error2 = new OutcomeError("E001", "Error 1");

        // Act
        var isEqual = error1.Equals(error2);

        // Assert
        Assert.True(isEqual);
    }

    [Fact]
    public void GetHashCode_ShouldReturnSameHashForEqualOutcomeErrors()
    {
        // Arrange
        var error1 = new OutcomeError("E001", "Error 1");
        var error2 = new OutcomeError("E001", "Error 1");

        // Act
        var hash1 = error1.GetHashCode();
        var hash2 = error2.GetHashCode();

        // Assert
        Assert.Equal(hash1, hash2);
    }
}
