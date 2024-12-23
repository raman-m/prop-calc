﻿using RamanM.Properti.Calculator.Implementations;
using Xunit;

namespace RamanM.Properti.Calculator.Tests.Conversion;

public class DivisionConversionTests
{
    [Fact]
    public void Division_DoubleParamsCstr_ReturnsDivisionObject()
    {
        // Arrange, Act
        var sut = new Division(15, 3);

        // Assert
        Assert.NotNull(sut);
        Assert.Equal(5D, sut.ToResult());
    }

    [Fact]
    public void Division_MixedDoubleParamsCstr_ReturnsDivisionObject()
    {
        // Arrange, Act
        var sut = new Division(3, new Operation<double>(2));
        var sut2 = new Division(new Operation<double>(7), 2);

        // Assert
        Assert.NotNull(sut);
        Assert.NotNull(sut2);
        Assert.Equal(1.5D, sut.ToResult());
        Assert.Equal(3.5D, sut2.ToResult());
    }

    [Fact]
    public void Division_FullInterfaceParamsCstr_ReturnsDivisionObject()
    {
        // Arrange
        /*IOperation<double>*/ var param1 = new Division(3, 1);
        /*IOperation<double>*/ var param2 = new Division(2, 1);

        // Act
        var sut = new Division(param1, param2);

        // Assert
        Assert.NotNull(sut);
        Assert.Equal(1.5D, sut.ToResult());
    }

    [Fact]
    public void Division_ImplicitConversionOfTwoSum_ReturnsDivisionObject()
    {
        // Arrange, Act
        var sut = new Division(new Sum(4, 2), new Sum(1, 2));

        // Assert
        Assert.NotNull(sut);
        Assert.Equal(2D, sut.ToResult());
    }

    [Fact]
    public void Division_ImplicitConversionOfTwoSubtraction_ReturnsDivisionObject()
    {
        // Arrange, Act
        var sut = new Division(new Subtraction(5, 2), new Subtraction(3, 1));

        // Assert
        Assert.NotNull(sut);
        Assert.Equal(1.5D, sut.ToResult());
    }


    [Fact]
    public void Division_ImplicitConversionOfTwoMultiplication_ReturnsDivisionObject()
    {
        // Arrange, Act
        var sut = new Division(new Multiplication(1, 5), new Multiplication(1, 4));

        // Assert
        Assert.NotNull(sut);
        Assert.Equal(1.25D, sut.ToResult());
    }

    [Fact]
    public void Division_ImplicitConversionOfTwoDivision_ReturnsDivisionObject()
    {
        // Arrange, Act
        var sut = new Division(new Division(16, 2), new Division(12, 3));

        // Assert
        Assert.NotNull(sut);
        Assert.Equal(2D, sut.ToResult());
    }

    [Fact]
    public void Division_ImplicitConversionOfTwoFraction_ReturnsDivisionObject()
    {
        // Arrange, Act
        var sut = new Division(new Fraction(4, 2), new Fraction(8, 2));

        // Assert
        Assert.NotNull(sut);
        Assert.Equal(0.5D, sut.ToResult());
    }

    [Fact]
    public void Division_ImplicitConversionOfTwoFaculty_ReturnsDivisionObject()
    {
        // Arrange, Act
        var sut = new Division(new Faculty(4), new Faculty(3));

        // Assert
        Assert.NotNull(sut);
        Assert.Equal(4D, sut.ToResult());
    }
}
