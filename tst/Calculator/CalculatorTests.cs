using Moq;
using RamanM.Properti.Calculator.Interfaces;
using Xunit;

namespace RamanM.Properti.Calculator.Tests
{
    public class CalculatorTests
    {
        private readonly Calculator sut; // System Under Test
        private readonly Mock<IConsoleWriting> console;

        public CalculatorTests()
        {
            console = new Mock<IConsoleWriting>();
            sut = new Calculator(console.Object);
        }

        [Fact]
        public void Sum_TwoOperands_ReturnsTheSum()
        {
            // Arrange
            double operand1 = 1.0D;
            double operand2 = 2.5D;
            double expected = operand1 + operand2;

            // Act
            double actual = sut.Sum(operand1, operand2);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
