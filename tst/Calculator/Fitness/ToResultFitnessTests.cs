using RamanM.Properti.Calculator.Implementations;
using Xunit;

namespace RamanM.Properti.Calculator.Tests.Fitness;

public class ToResultFitnessTests
{
    [Fact(DisplayName = "new Sum(5.2, 1.5).toResult() should return 6.7")]
    public void Sum_ToResult_Example_ReturnsTheSum()
    {
        // Arrange
        var sut = new Sum(5.2, 1.5);
        double expected = 6.7;

        // Act
        var actual = sut.ToResult();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "new Division(30, new Sum(2, 3)).toResult() should return 6")]
    public void Division_ToResult_ExampleWithSum_ReturnsTheQuotient()
    {
        // Arrange
        var sut = new Division(30, new Sum(2, 3));
        double expected = 6;

        // Act
        var actual = sut.ToResult();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "new Faculty(4).toResult() should return 24")]
    public void Faculty_ToResult_Example_ReturnsTheFactorial()
    {
        // Arrange
        var sut = new Faculty(4);
        long expected = 24;

        // Act
        long actual = (long)sut.ToResult();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "new Multiplication(new Fraction(9,4), new Fraction(2,3)).toResult() should return 1.5")]
    public void Multiplication_ToResult_ExampleWithFractions_ReturnsTheProduct()
    {
        // Arrange
        var sut = new Multiplication(new Fraction(9, 4), new Fraction(2, 3));
        double expected = 1.5;

        // Act
        var actual = sut.ToResult();

        // Assert
        Assert.Equal(expected, actual);
    }
}
