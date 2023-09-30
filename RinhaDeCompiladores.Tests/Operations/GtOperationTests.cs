using RinhaDeCompiladores.Operations;

namespace RinhaDeCompiladores.Tests.Operations;

public class GtOperationTests
{
    [Fact]
    public void Gt_FirstNumberGreaterThanSecondNumber()
    {
        var operation = new GtOperation();

        var result = operation.Execute("5", "1");

        Assert.True(result);
    }

    [Fact]
    public void Gt_FirstNumberIsEqualThanSecondNumber()
    {
        var operation = new GtOperation();

        var result = operation.Execute("5", "5");

        Assert.False(result);
    }

    [Fact]
    public void Gt_SecondNumberGreaterThanFirstNumber()
    {
        var operation = new GtOperation();

        var result = operation.Execute("5", "10");

        Assert.False(result);
    }

    [Fact]
    public void Gt_OneNumberAndOneLetter()
    {
        var operation = new GtOperation();

        Assert.Throws<InvalidOperationException>(() => operation.Execute("1", "a"));
    }
}