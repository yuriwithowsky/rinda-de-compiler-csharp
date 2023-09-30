using RinhaDeCompiladores.Operations;

namespace RinhaDeCompiladores.Tests.Operations;

public class MulOperationTests
{
    [Fact]
    public void Mul_TwoNumbers()
    {
        var operation = new MulOperation();

        var result = operation.Execute("5", "5");

        Assert.Equal(expected: 25, actual: result);
    }

    [Fact]
    public void Mul_OneNumberAndOneLetter()
    {
        var operation = new MulOperation();

        Assert.Throws<InvalidOperationException>(() => operation.Execute("1", "a"));
    }
}
