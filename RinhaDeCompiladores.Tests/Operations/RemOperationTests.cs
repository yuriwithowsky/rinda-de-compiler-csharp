using RinhaDeCompiladores.Operations;

namespace RinhaDeCompiladores.Tests.Operations;

public class RemOperationTests
{
    [Fact]
    public void Rem_TwoNumbers()
    {
        var operation = new RemOperation();

        var result = operation.Execute("10", "10");

        Assert.Equal(expected: "0", result);
    }

    [Fact]
    public void Rem_OneNumberAndOneLetter()
    {
        var operation = new RemOperation();

        Assert.Throws<InvalidOperationException>(() => operation.Execute("1", "a"));
    }
}
