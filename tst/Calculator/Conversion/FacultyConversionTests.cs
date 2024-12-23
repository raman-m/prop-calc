using RamanM.Properti.Calculator.Implementations;
using Xunit;

namespace RamanM.Properti.Calculator.Tests.Conversion;

public class FacultyConversionTests
{
    [Fact]
    public void Faculty_SingleParamCstr_ReturnsFacultyObject()
    {
        // Arrange, Act
        var sut = new Faculty(4);

        // Assert
        Assert.NotNull(sut);
        Assert.Equal(24L, sut.ToResult());
    }

    [Fact]
    public void Faculty_ConstantParamCstr_ReturnsFacultyObject()
    {
        // Arrange, Act
        var sut = new Faculty(new Operation<long>(3));

        // Assert
        Assert.NotNull(sut);
        Assert.Equal(6L, sut.ToResult());
    }

    [Fact]
    public void Faculty_InterfaceParamCstr_ReturnsFacultyObject()
    {
        // Arrange
        /*IOperation<long>*/ var param = new Operation<long>(2);

        // Act
        var sut = new Faculty(param);

        // Assert
        Assert.NotNull(sut);
        Assert.Equal(2L, sut.ToResult());
    }

    [Fact]
    public void Faculty_ImplicitConversionOfSum_ReturnsFacultyObject()
    {
        // Arrange, Act
        var sut = new Faculty(new Sum(2, 3));

        // Assert
        Assert.NotNull(sut);
        Assert.Equal(120L, sut.ToResult());
    }

    [Fact]
    public void Faculty_ImplicitConversionOfSubtraction_ReturnsFacultyObject()
    {
        // Arrange, Act
        var sut = new Faculty(new Subtraction(5, 2));

        // Assert
        Assert.NotNull(sut);
        Assert.Equal(6L, sut.ToResult());
    }

    [Fact]
    public void Faculty_ImplicitConversionOfMultiplication_ReturnsFacultyObject()
    {
        // Arrange, Act
        var sut = new Faculty(new Multiplication(2.5, 2));

        // Assert
        Assert.NotNull(sut);
        Assert.Equal(120L, sut.ToResult());
    }

    [Fact]
    public void Faculty_ImplicitConversionOfDivision_ReturnsFacultyObject()
    {
        // Arrange, Act
        var sut = new Faculty(new Division(12, 2));

        // Assert
        Assert.NotNull(sut);
        Assert.Equal(720L, sut.ToResult());
    }

    [Fact]
    public void Faculty_ImplicitConversionOfFraction_ReturnsFacultyObject()
    {
        // Arrange, Act
        var sut = new Faculty(new Fraction(10, 2));

        // Assert
        Assert.NotNull(sut);
        Assert.Equal(120L, sut.ToResult());
    }

    [Fact]
    public void Faculty_ImplicitConversionOfFaculty_ReturnsFacultyObject()
    {
        // Arrange, Act
        var sut = new Faculty(new Faculty(3));

        // Assert
        Assert.NotNull(sut);
        Assert.Equal(720L, sut.ToResult());
    }
}
