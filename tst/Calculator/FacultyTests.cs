using RamanM.Properti.Calculator.Implementations;
using Xunit;

namespace RamanM.Properti.Calculator.Tests;

public class FacultyTests
{
    [Fact]
    public void ToResult_Example_ReturnsTheFactorial()
    {
        // Arrange
        var sut = new Faculty(4);
        long expected = 1 * 2 * 3 * 4;

        // Act
        long actual = (long)sut.ToResult();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Print_NoParent_PrintsFinalEquality()
    {
        // Arrange
        int operand = 4;
        var sut = new Faculty(operand);
        long factorial = 1 * 2 * 3 * 4;

        string expected = $"({operand}!) = {factorial}";

        // Act
        var actual = sut.Print();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Print_WithParent_PrintsWithoutResult()
    {
        int operand = 4;
        var sut = new Faculty(operand);
        sut.Parent = new Operation(() => sut.ToResult());
        string expected = $"({operand}!)";

        // Act
        var actual = sut.Print();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void PrintSentence_NoParent_PrintsFinalSentence()
    {
        // Arrange
        int operand = 4;
        var sut = new Faculty(operand);
        long factorial = 1 * 2 * 3 * 4;

        string expected = $"{nameof(Faculty).ToLower()} of {operand} is {factorial}";

        // Act
        var actual = sut.PrintSentence();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void PrintSentence_WithParent_PrintsWell()
    {
        // Arrange
        int operand = 4;
        var sut = new Faculty(operand);
        sut.Parent = new Operation(() => sut.ToResult());

        string expected = $"{nameof(Faculty).ToLower()} of {operand}";

        // Act
        var actual = sut.PrintSentence();

        // Assert
        Assert.Equal(expected, actual);
    }
}
