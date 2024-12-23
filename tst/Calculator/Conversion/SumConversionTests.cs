﻿using RamanM.Properti.Calculator.Implementations;
using Xunit;

namespace RamanM.Properti.Calculator.Tests.Conversion;

public class SumConversionTests
{
    [Fact]
    public void Sum_IntParamsCstr_ReturnsSumObject()
    {
        // Arrange, Act
        var sut = new Sum(2, 3);

        // Assert
        Assert.NotNull(sut);
        Assert.Equal(5D, sut.ToResult());
    }

    [Fact]
    public void Sum_DoubleParamsCstr_ReturnsSumObject()
    {
        // Arrange, Act
        var sut = new Sum(2.5D, 3.0D);

        // Assert
        Assert.NotNull(sut);
        Assert.Equal(5.5D, sut.ToResult());
    }

    [Fact]
    public void Sum_MixedDoubleParamsCstr_ReturnsSumObject()
    {
        // Arrange, Act
        var sut = new Sum(2, new Operation(1));
        var sut2 = new Sum(new Operation(1), 7);

        // Assert
        Assert.NotNull(sut);
        Assert.NotNull(sut2);
        Assert.Equal(3D, sut.ToResult());
        Assert.Equal(8D, sut2.ToResult());
    }

    [Fact]
    public void Sum_FullInterfaceParamsCstr_ReturnsSumObject()
    {
        // Arrange
        /*IOperation<double>*/ var param1 = new Sum(2, 3);
        /*IOperation<double>*/ var param2 = new Sum(4, 5);

        // Act
        var sut = new Sum(param1, param2);

        // Assert
        Assert.NotNull(sut);
        Assert.Equal(14.0D, sut.ToResult());
    }

    [Fact]
    public void Sum_ImplicitConversionOfTwoSum_ReturnsSumObject()
    {
        // Arrange, Act
        var sut = new Sum(new Sum(2, 3), new Sum(4, 5));

        // Assert
        Assert.NotNull(sut);
        Assert.Equal(14D, sut.ToResult());
    }

    [Fact]
    public void Sum_ImplicitConversionOfTwoSubtraction_ReturnsSumObject()
    {
        // Arrange, Act
        var sut = new Sum(new Subtraction(3, 2), new Subtraction(5, 3));

        // Assert
        Assert.NotNull(sut);
        Assert.Equal(3D, sut.ToResult());
    }

    [Fact]
    public void Sum_ImplicitConversionOfTwoMultiplication_ReturnsSumObject()
    {
        // Arrange, Act
        var sut = new Sum(new Multiplication(3, 2), new Multiplication(5, 2));

        // Assert
        Assert.NotNull(sut);
        Assert.Equal(16D, sut.ToResult());
    }

    [Fact]
    public void Sum_ImplicitConversionOfTwoDivision_ReturnsSumObject()
    {
        // Arrange, Act
        var sut = new Sum(new Division(15, 5), new Division(15, 3));

        // Assert
        Assert.NotNull(sut);
        Assert.Equal(8D, sut.ToResult());
    }

    [Fact]
    public void Sum_ImplicitConversionOfTwoFraction_ReturnsSumObject()
    {
        // Arrange, Act
        var sut = new Sum(new Fraction(1, 3), new Fraction(1, 6));

        // Assert
        Assert.NotNull(sut);
        Assert.Equal(0.5, sut.ToResult());
    }

    [Fact]
    public void Sum_ImplicitConversionOfTwoFaculty_ReturnsSumObject()
    {
        // Arrange, Act
        var sut = new Sum(new Faculty(3), new Faculty(4));

        // Assert
        Assert.NotNull(sut);
        Assert.Equal(30D, sut.ToResult());
    }

    [Fact]
    public void Sum_ImplicitConversionOfFacultyAndConst_ReturnsSumObject()
    {
        // Arrange, Act
        var sut = new Sum(new Faculty(8), 1);

        // Assert
        Assert.NotNull(sut);
        Assert.Equal(40321D, sut.ToResult());
    }
}
