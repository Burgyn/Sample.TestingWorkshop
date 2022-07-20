using System;
using FluentAssertions;
using Xunit;

namespace MutationTesting.UnitTests;

public class CalculatorShould
{
    [Theory]
    [InlineData(5, 5, 10)]
    public void Add(int a, int b, int expected)
    {
        var calculator = new Calculator();
        var result = calculator.Add(a, b);
        result.Should().Be(expected);
    }
    
    [Theory]
    [InlineData(5, 5, 0)]
    public void Subtract(int a, int b, int expected)
    {
        var calculator = new Calculator();
        var result = calculator.Subtract(a, b);
        result.Should().Be(expected);
    }
    
    [Theory]
    [InlineData(1, 1, 1)]
    public void Multiply(int a, int b, int expected)
    {
        var calculator = new Calculator();
        var result = calculator.Multiply(a, b);
        result.Should().Be(expected);
    }
    
    [Theory]
    [InlineData(1, 1, 1, 0)]
    public void Divide(int a, int b, int expected, int remainder)
    {
        var calculator = new Calculator();
        var result = calculator.Divide(a, b);
        result.Result.Should().Be(expected);
        result.Remainder.Should().Be(remainder);
    }
    
    [Fact]
    public void ThrowExceptionWhenDivideByZero()
    {
        var calculator = new Calculator();
        Action act = () => calculator.Divide(1, 0);
        act.Should().Throw<DivideByZeroException>();
    }
}