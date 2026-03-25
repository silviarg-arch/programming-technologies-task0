using ProgrammingTechnologiesTask0.Logic;

namespace ProgrammingTechnologiesTask0.Tests;

public class CalculatorTests
{
    [Fact]
    public void AddToBaseNumber_ShouldReturnCorrectResult()
    {
        var calculator = new Calculator();

        var result = calculator.AddToBaseNumber(3);

        Assert.Equal(8, result);
    }
}