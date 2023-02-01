namespace RamanM.Properti.Calculator.Tests;

public class CalculatorTests
{
    private readonly Calculator sut; // System Under Test

    public CalculatorTests()
    {
        sut = new Calculator();
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
