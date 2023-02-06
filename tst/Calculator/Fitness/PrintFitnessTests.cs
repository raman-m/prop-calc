using RamanM.Properti.Calculator.Implementations;
using Xunit;

namespace RamanM.Properti.Calculator.Tests.Fitness
{
    public class PrintFitnessTests
    {
        [Fact(DisplayName = "new Sum(5.2, 1.5).print() should return '(5.2 + 1.5) = 6.7'")]
        public void Sum_Print_Example_PrintsFinalEquality()
        {
            // Arrange
            var sut = new Sum(5.2, 1.5);
            string expected = "(5.2 + 1.5) = 6.7";

            // Act
            var actual = sut.Print();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact(DisplayName = "new Division(30, new Sum(2, 3)).print() should return '(30 / (2 + 3)) = 6'")]
        public void Division_Print_ExampleWithSum_PrintsFinalEquality()
        {
            // Arrange
            var sut = new Division(30, new Sum(2, 3));
            string expected = "(30 / (2 + 3)) = 6";

            // Act
            var actual = sut.Print();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact(DisplayName = "new Faculty(4).print() should return '(4!) = 24'")]
        public void Faculty_Print_Example_PrintsFinalEquality()
        {
            // Arrange
            var sut = new Faculty(4);
            string expected = "(4!) = 24";

            // Act
            var actual = sut.Print();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact(DisplayName = "new Multiplication(new Fraction(9,4), new Fraction(2,3)).print() should return '((9/4) * (2/3)) = 1.5'")]
        public void Multiplication_Print_ExampleWithFractions_PrintsFinalEquality()
        {
            // Arrange
            var sut = new Multiplication(new Fraction(9, 4), new Fraction(2, 3));
            string expected = "((9/4) * (2/3)) = 1.5";

            // Act
            var actual = sut.Print();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
