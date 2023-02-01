using RamanM.Properti.Calculator.Implementations;

namespace RamanM.Properti.Calculator.Tests;

public class MultiplicationTests
{
    [Fact]
    public void ToResult_Example_ReturnsTheProduct()
    {
        // Arrange
        var sut = new Multiplication(5.0, 1.1);
        double expected = 5.5;

        // Act
        var actual = sut.ToResult();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Print_NoParent_PrintsFinalEquality()
    {
        // Arrange
        double left = 5.0D, right = 1.1D, product = 5.5D;
        var sut = new Multiplication(left, right);
        string expected = $"({left} * {right}) = {product}";

        // Act
        var actual = sut.Print();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Print_WithParent_PrintsWithoutResult()
    {
        // Arrange
        double left = 5.0D, right = 1.1D;
        var sut = new Multiplication(left, right);
        sut.Parent = new Constant(0);
        string expected = $"({left} * {right})";

        // Act
        var actual = sut.Print();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void PrintSentence_NoParent_PrintsFinalSentence()
    {
        // Arrange
        double left = 5.0D, right = 1.1D, product = 5.5D;
        var sut = new Multiplication(left, right);
        string expected = $"{nameof(Multiplication).ToLower()} of {left} and {right} is {product}";

        // Act
        var actual = sut.PrintSentence();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void PrintSentence_WithParent_PrintsWell()
    {
        // Arrange
        double left = 5.0D, right = 1.1D;
        var sut = new Multiplication(left, right);
        sut.Parent = new Constant(0);
        string expected = $"{nameof(Multiplication).ToLower()} of {left} and {right}";

        // Act
        var actual = sut.PrintSentence();

        // Assert
        Assert.Equal(expected, actual);
    }
}
