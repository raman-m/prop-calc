using RamanM.Properti.Calculator.Implementations;
using RamanM.Properti.Calculator.Interfaces;
using Xunit;

namespace RamanM.Properti.Calculator.Tests.Conversion
{
    public class SubtractionConversionTests
    {
        [Fact]
        public void Subtraction_DoubleParamsCstr_ReturnsSubtractionObject()
        {
            // Arrange, Act
            var sut = new Subtraction(5, 3);

            // Assert
            Assert.NotNull(sut);
            Assert.Equal(2, sut.ToResult());
        }

        [Fact]
        public void Subtraction_MixedDoubleParamsCstr_ReturnsSubtractionObject()
        {
            // Arrange, Act
            var sut = new Subtraction(3, new Constant<double>(1));
            var sut2 = new Subtraction(new Constant<double>(1), 7);

            // Assert
            Assert.NotNull(sut);
            Assert.NotNull(sut2);
            Assert.Equal(2, sut.ToResult());
            Assert.Equal(-6, sut2.ToResult());
        }

        [Fact]
        public void Subtraction_FullInterfaceParamsCstr_ReturnsSubtractionObject()
        {
            // Arrange
            /*IOperation<double>*/ var param1 = new Subtraction(5, 2);
            /*IOperation<double>*/ var param2 = new Subtraction(5, 4);

            // Act
            var sut = new Subtraction(param1, param2);

            // Assert
            Assert.NotNull(sut);
            Assert.Equal(2, sut.ToResult());
        }

        [Fact]
        public void Subtraction_ImplicitConversionOfTwoSubtraction_ReturnsSubtractionObject()
        {
            // Arrange, Act
            var sut = new Subtraction(new Subtraction(7, 2), new Subtraction(5, 3));

            // Assert
            Assert.NotNull(sut);
            Assert.Equal(3, sut.ToResult());
        }

        [Fact]
        public void Subtraction_ImplicitConversionOfTwoSum_ReturnsSubtractionObject()
        {
            // Arrange, Act
            var sut = new Subtraction(new Sum(3, 2), new Sum(5, 3));

            // Assert
            Assert.NotNull(sut);
            Assert.Equal(-3, sut.ToResult());
        }

        [Fact]
        public void Subtraction_ImplicitConversionOfTwoMultiplication_ReturnsSubtractionObject()
        {
            // Arrange, Act
            var sut = new Subtraction(new Multiplication(3, 2), new Multiplication(5, 2));

            // Assert
            Assert.NotNull(sut);
            Assert.Equal(-4, sut.ToResult());
        }

        [Fact]
        public void Subtraction_ImplicitConversionOfTwoDivision_ReturnsSubtractionObject()
        {
            // Arrange, Act
            var sut = new Subtraction(new Division(15, 3), new Division(15, 5));

            // Assert
            Assert.NotNull(sut);
            Assert.Equal(2, sut.ToResult());
        }

        [Fact]
        public void Subtraction_ImplicitConversionOfTwoFraction_ReturnsSubtractionObject()
        {
            // Arrange, Act
            var sut = new Subtraction(new Fraction(4, 3), new Fraction(1, 3));

            // Assert
            Assert.NotNull(sut);
            Assert.Equal(1.0D, sut.ToResult());
        }

        [Fact]
        public void Subtraction_ImplicitConversionOfTwoFaculty_ReturnsSubtractionObject()
        {
            // Arrange, Act
            var sut = new Subtraction(new Faculty(3), new Faculty(4));

            // Assert
            Assert.NotNull(sut);
            Assert.Equal(-18, sut.ToResult());
        }
    }
}
