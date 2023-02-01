using RamanM.Properti.Calculator.Implementations;

namespace RamanM.Properti.Calculator.Tests;

public class SumTests
{
    [Fact]
    public void ToResult_Example_ReturnsTheSum()
    {
        // Arrange
        var sut = new Sum(5.2, 1.5);
        double expected = 6.7;

        // Act
        var actual = sut.ToResult();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Print_NoParent_PrintsFinalEquality()
    {
        // Arrange
        double left = 5.2D, right = 1.5D, sum = 6.7D;
        var sut = new Sum(left, right);
        string expected = $"({left} + {right}) = {sum}";

        // Act
        var actual = sut.Print();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Print_WithParent_PrintsWithoutResult()
    {
        // Arrange
        double left = 5.2D, right = 1.5D;
        var sut = new Sum(left, right);
        sut.Parent = new Constant(0);
        string expected = $"({left} + {right})";

        // Act
        var actual = sut.Print();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void PrintSentence_NoParent_PrintsFinalSentence()
    {
        // Arrange
        double left = 5.2D, right = 1.5D, sum = 6.7D;
        var sut = new Sum(left, right);
        string expected = $"{nameof(Sum).ToLower()} of {left} and {right} is {sum}";

        // Act
        var actual = sut.PrintSentence();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void PrintSentence_WithParent_PrintsWell()
    {
        // Arrange
        double left = 5.2D, right = 1.5D;
        var sut = new Sum(left, right);
        sut.Parent = new Constant(0);
        string expected = $"{nameof(Sum).ToLower()} of {left} and {right}";

        // Act
        var actual = sut.PrintSentence();

        // Assert
        Assert.Equal(expected, actual);
    }
}
