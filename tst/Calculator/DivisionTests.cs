using RamanM.Properti.Calculator.Implementations;

namespace RamanM.Properti.Calculator.Tests;

public class DivisionTests
{
    [Fact]
    public void ToResult_Example_ReturnsTheQuotient()
    {
        // Arrange
        var sut = new Division(30, 5);
        double expected = 6;

        // Act
        var actual = sut.ToResult();

        // Assert
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void Print_NoParent_PrintsFinalEquality()
    {
        // Arrange
        double left = 30D, right = 5D, quotient = 6D;
        var sut = new Division(left, right);
        string expected = $"({left} / {right}) = {quotient}";

        // Act
        var actual = sut.Print();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Print_WithParent_PrintsWithoutResult()
    {
        // Arrange
        double left = 30D, right = 5D;
        var sut = new Division(left, right);
        sut.Parent = new Constant(0);
        string expected = $"({left} / {right})";

        // Act
        var actual = sut.Print();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void PrintSentence_NoParent_PrintsFinalSentence()
    {
        // Arrange
        double left = 30D, right = 5D, quotient = 6D;
        var sut = new Division(left, right);
        string expected = $"{nameof(Division).ToLower()} of {left} and {right} is {quotient}";

        // Act
        var actual = sut.PrintSentence();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void PrintSentence_WithParent_PrintsWell()
    {
        // Arrange
        double left = 30D, right = 5D;
        var sut = new Division(left, right);
        sut.Parent = new Constant(0);
        string expected = $"{nameof(Division).ToLower()} of {left} and {right}";

        // Act
        var actual = sut.PrintSentence();

        // Assert
        Assert.Equal(expected, actual);
    }
}
