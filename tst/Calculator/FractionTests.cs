using RamanM.Properti.Calculator.Implementations;

namespace RamanM.Properti.Calculator.Tests;

public class FractionTests
{
    [Fact]
    public void ToResult_Example_ReturnsTheFraction()
    {
        // Arrange
        var sut = new Fraction(2, 3);
        double expected = 2D / 3D;

        // Act
        var actual = sut.ToResult();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Print_NoParent_PrintsFinalEquality()
    {
        // Arrange
        int left = 2, right = 3;
        double fraction = 2D / 3D;
        var sut = new Fraction(left, right);
        string expected = $"({left}/{right}) = {fraction}";

        // Act
        var actual = sut.Print();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Print_WithParent_PrintsWithoutResult()
    {
        // Arrange
        int left = 2, right = 3;
        var sut = new Fraction(left, right);
        sut.Parent = new Constant(0);
        string expected = $"({left}/{right})";

        // Act
        var actual = sut.Print();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void PrintSentence_NoParent_PrintsFinalSentence()
    {
        // Arrange
        int left = 2, right = 3;
        double fraction = 2D / 3D;
        var sut = new Fraction(left, right);
        string expected = $"{nameof(Fraction).ToLower()} of {left} and {right} is {fraction}";

        // Act
        var actual = sut.PrintSentence();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void PrintSentence_WithParent_PrintsWell()
    {
        // Arrange
        int left = 2, right = 3;
        var sut = new Fraction(left, right);
        sut.Parent = new Constant(0);
        string expected = $"{left}/{right}";

        // Act
        var actual = sut.PrintSentence();

        // Assert
        Assert.Equal(expected, actual);
    }
}
