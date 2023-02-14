using RamanM.Properti.Calculator.Implementations;
using Xunit;

namespace RamanM.Properti.Calculator.Tests
{
    public class SubtractionTests
    {
        [Fact]
        public void ToResult_Example_ReturnsTheDifference()
        {
            // Arrange
            var sut = new Subtraction(5.5, 1.3);
            double expected = 4.2;

            // Act
            var actual = sut.ToResult();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Print_NoParent_PrintsFinalEquality()
        {
            // Arrange
            double left = 5.5D, right = 1.3D, difference = 4.2D;
            var sut = new Subtraction(left, right);
            string expected = $"({left} - {right}) = {difference}";

            // Act
            var actual = sut.Print();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Print_WithParent_PrintsWithoutResult()
        {
            // Arrange
            double left = 5.5D, right = 1.3D;
            var sut = new Subtraction(left, right);
            sut.Parent = new Operation(() => sut.ToResult());
            string expected = $"({left} - {right})";

            // Act
            var actual = sut.Print();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PrintSentence_NoParent_PrintsFinalSentence()
        {
            // Arrange
            double left = 5.5D, right = 1.3D, difference = 4.2D;
            var sut = new Subtraction(left, right);
            string expected = $"{nameof(Subtraction).ToLower()} of {left} and {right} is {difference}";

            // Act
            var actual = sut.PrintSentence();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PrintSentence_WithParent_PrintsWell()
        {
            // Arrange
            double left = 5.5D, right = 1.3D;
            var sut = new Subtraction(left, right);
            sut.Parent = new Operation(() => sut.ToResult());
            string expected = $"{nameof(Subtraction).ToLower()} of {left} and {right}";

            // Act
            var actual = sut.PrintSentence();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
