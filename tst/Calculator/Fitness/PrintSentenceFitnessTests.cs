using RamanM.Properti.Calculator.Implementations;
using Xunit;

namespace RamanM.Properti.Calculator.Tests.Fitness
{
    public class PrintSentenceFitnessTests
    {
        [Fact(DisplayName = "new Sum(5.2, 1.5).printSentence() should return 'sum of 5.2 and 1.5 is 6.7'")]
        public void Sum_PrintSentence_Example_PrintsFinalSentence()
        {
            // Arrange
            var sut = new Sum(5.2, 1.5);
            string expected = "sum of 5.2 and 1.5 is 6.7";

            // Act
            var actual = sut.PrintSentence();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact(DisplayName = "new Division(30, new Sum(2, 3)).printSentence() should return 'division of 30 by sum of 2 and 3 is 6'")]
        public void Division_PrintSentence_ExampleWithSum_PrintsFinalSentence()
        {
            // Arrange
            var sut = new Division(30, new Sum(2, 3));
            string expected = "division of 30 by sum of 2 and 3 is 6";

            // Act
            var actual = sut.PrintSentence();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact(DisplayName = "new Faculty(4).printSentence() should return 'faculty of 4 is 24'")]
        public void Faculty_PrintSentence_Example_PrintsFinalSentence()
        {
            // Arrange
            var sut = new Faculty(4);
            string expected = "faculty of 4 is 24";

            // Act
            var actual = sut.PrintSentence();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact(DisplayName = "new Multiplication(new Fraction(9,4), new Fraction(2,3)).printSentence() should return 'multiplication of 9/4 and 2/3 is 1.5'")]
        public void Multiplication_PrintSentence_ExampleWithFractions_PrintsFinalSentence()
        {
            // Arrange
            var sut = new Multiplication(new Fraction(9, 4), new Fraction(2, 3));
            string expected = "multiplication of 9/4 and 2/3 is 1.5";

            // Act
            var actual = sut.PrintSentence();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
