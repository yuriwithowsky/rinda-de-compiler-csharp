using RinhaDeCompiladores.Operations;

namespace RinhaDeCompiladores.Tests.Operations;

public class DivOperationTests
{
    [Fact]
    public void Div_TwoNumbers()
    {
        var operation = new DivOperation();

        var result = operation.Execute("10", "10");

        Assert.Equal(expected: "1", result);
    }

    [Fact]
    public void Div_OneNumberAndOneLetter()
    {
        var operation = new DivOperation();

        Assert.Throws<InvalidOperationException>(() => operation.Execute("1", "a"));
    }

    [Fact]
    public void Div_ByZero()
    {
        var operation = new DivOperation();

        Assert.Throws<DivideByZeroException>(() => operation.Execute("1", "0"));
    }
}
