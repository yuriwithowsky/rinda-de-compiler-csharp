using RinhaDeCompiladores.Operations;

namespace RinhaDeCompiladores.Tests.Operations;

public class GtOperationTests
{
    [Fact]
    public void Gt_FirstNumberGreaterThanSecondNumber()
    {
        var operation = new GtOperation();

        var result = operation.Execute("5", "1");

        Assert.Equal(expected: "true", result);
    }

    [Fact]
    public void Gt_FirstNumberIsEqualThanSecondNumber()
    {
        var operation = new GtOperation();

        var result = operation.Execute("5", "5");

        Assert.Equal(expected: "false", result);
    }

    [Fact]
    public void Gt_SecondNumberGreaterThanFirstNumber()
    {
        var operation = new GtOperation();

        var result = operation.Execute("5", "10");

        Assert.Equal(expected: "false", result);
    }

    [Fact]
    public void Gt_OneNumberAndOneLetter()
    {
        var operation = new GtOperation();

        Assert.Throws<InvalidOperationException>(() => operation.Execute("1", "a"));
    }
}