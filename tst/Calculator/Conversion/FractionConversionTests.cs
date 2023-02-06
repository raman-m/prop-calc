using RamanM.Properti.Calculator.Implementations;
using RamanM.Properti.Calculator.Interfaces;
using Xunit;

namespace RamanM.Properti.Calculator.Tests.Conversion
{
    public class FractionConversionTests
    {
        [Fact]
        public void Fraction_DoubleParamsCstr_ReturnsFractionObject()
        {
            // Arrange, Act
            var sut = new Fraction(1, 2);

            // Assert
            Assert.NotNull(sut);
            Assert.Equal(0.5, sut.ToResult());
        }

        [Fact]
        public void Fraction_MixedDoubleParamsCstr_ReturnsFractionObject()
        {
            // Arrange, Act
            var sut = new Fraction(1, new Constant<int>(2));
            var sut2 = new Fraction(new Constant<int>(1), 4);

            // Assert
            Assert.NotNull(sut);
            Assert.NotNull(sut2);
            Assert.Equal(0.5, sut.ToResult());
            Assert.Equal(0.25, sut2.ToResult());
        }

        [Fact]
        public void Fraction_FullInterfaceParamsCstr_ReturnsFractionObject()
        {
            // Arrange
            IOperation<int> param1 = new Constant<int>(1);
            IOperation<int> param2 = new Constant<int>(2);

            // Act
            var sut = new Fraction(param1, param2);

            // Assert
            Assert.NotNull(sut);
            Assert.Equal(0.5, sut.ToResult());
        }

        [Fact]
        public void Fraction_ImplicitConversionOfTwoSum_ReturnsFractionObject()
        {
            // Arrange, Act
            var sut = new Fraction(new Sum(3, -2), new Sum(1, 3));

            // Assert
            Assert.NotNull(sut);
            Assert.Equal(0.25, sut.ToResult());
        }

        [Fact]
        public void Fraction_ImplicitConversionOfTwoSubtraction_ReturnsFractionObject()
        {
            // Arrange, Act
            var sut = new Fraction(new Subtraction(5, 2), new Subtraction(3, 1));

            // Assert
            Assert.NotNull(sut);
            Assert.Equal(1.5, sut.ToResult());
        }

        [Fact]
        public void Fraction_ImplicitConversionOfTwoMultiplication_ReturnsFractionObject()
        {
            // Arrange, Act
            var sut = new Fraction(new Multiplication(2, 5), new Multiplication(1, 4));

            // Assert
            Assert.NotNull(sut);
            Assert.Equal(2.5, sut.ToResult());
        }

        [Fact]
        public void Fraction_ImplicitConversionOfTwoDivision_ReturnsFractionObject()
        {
            // Arrange, Act
            var sut = new Fraction(new Division(16, 2), new Division(12, 3));

            // Assert
            Assert.NotNull(sut);
            Assert.Equal(2D, sut.ToResult());
        }

        [Fact]
        public void Fraction_ImplicitConversionOfTwoFraction_ReturnsFractionObject()
        {
            // Arrange, Act
            var sut = new Fraction(new Fraction(4, 2), new Fraction(8, 2));

            // Assert
            Assert.NotNull(sut);
            Assert.Equal(0.5, sut.ToResult());
        }

        [Fact]
        public void Fraction_ImplicitConversionOfTwoFaculty_ReturnsFractionObject()
        {
            // Arrange, Act
            var sut = new Fraction(new Faculty(3), new Faculty(4));

            // Assert
            Assert.NotNull(sut);
            Assert.Equal(0.25, sut.ToResult());
        }
    }
}
