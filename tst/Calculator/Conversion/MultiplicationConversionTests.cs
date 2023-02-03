using RamanM.Properti.Calculator.Implementations;
using RamanM.Properti.Calculator.Interfaces;

namespace RamanM.Properti.Calculator.Tests.Conversion;

public class MultiplicationConversionTests
{
    [Fact]
    public void Multiplication_DoubleParamsCstr_ReturnsMultiplicationObject()
    {
        // Arrange, Act
        var sut = new Multiplication(5, 3);

        // Assert
        Assert.NotNull(sut);
        Assert.Equal(15, sut.ToResult());
    }

    [Fact]
    public void Multiplication_MixedDoubleParamsCstr_ReturnsMultiplicationObject()
    {
        // Arrange, Act
        var sut = new Multiplication(3, new Constant<double>(1));
        var sut2 = new Multiplication(new Constant<double>(1), 7);

        // Assert
        Assert.NotNull(sut);
        Assert.NotNull(sut2);
        Assert.Equal(3, sut.ToResult());
        Assert.Equal(7, sut2.ToResult());
    }

    [Fact]
    public void Multiplication_FullInterfaceParamsCstr_ReturnsMultiplicationObject()
    {
        // Arrange
        IOperation<double> param1 = new Multiplication(2, 3);
        IOperation<double> param2 = new Multiplication(2, 4);

        // Act
        var sut = new Multiplication(param1, param2);

        // Assert
        Assert.NotNull(sut);
        Assert.Equal(48, sut.ToResult());
    }

    [Fact]
    public void Multiplication_ImplicitConversionOfTwoSum_ReturnsMultiplicationObject()
    {
        // Arrange, Act
        var sut = new Multiplication(new Sum(1, 2), new Sum(1, 3));

        // Assert
        Assert.NotNull(sut);
        Assert.Equal(12, sut.ToResult());
    }

    [Fact]
    public void Multiplication_ImplicitConversionOfTwoSubtraction_ReturnsMultiplicationObject()
    {
        // Arrange, Act
        var sut = new Multiplication(new Subtraction(3, 1), new Subtraction(5, 2));

        // Assert
        Assert.NotNull(sut);
        Assert.Equal(6, sut.ToResult());
    }

    [Fact]
    public void Multiplication_ImplicitConversionOfTwoMultiplication_ReturnsMultiplicationObject()
    {
        // Arrange, Act
        var sut = new Multiplication(new Multiplication(1, 3), new Multiplication(1, 4));

        // Assert
        Assert.NotNull(sut);
        Assert.Equal(12, sut.ToResult());
    }

    [Fact]
    public void Multiplication_ImplicitConversionOfTwoDivision_ReturnsMultiplicationObject()
    {
        // Arrange, Act
        var sut = new Multiplication(new Division(16, 4), new Division(15, 5));

        // Assert
        Assert.NotNull(sut);
        Assert.Equal(12, sut.ToResult());
    }

    [Fact]
    public void Multiplication_ImplicitConversionOfTwoFraction_ReturnsMultiplicationObject()
    {
        // Arrange, Act
        var sut = new Multiplication(new Fraction(1, 2), new Fraction(4, 2));

        // Assert
        Assert.NotNull(sut);
        Assert.Equal(1.0D, sut.ToResult());
    }

    [Fact]
    public void Multiplication_ImplicitConversionOfTwoFaculty_ReturnsMultiplicationObject()
    {
        // Arrange, Act
        var sut = new Multiplication(new Faculty(2), new Faculty(4));

        // Assert
        Assert.NotNull(sut);
        Assert.Equal(48, sut.ToResult());
    }
}
