using RinhaDeCompiladores.Operations;

namespace RinhaDeCompiladores.Tests.Operations;

public class SubOperationTests
{
    [Fact]
    public void Sub_TwoNumbers()
    {
        var operation = new SubOperation();

        var result = operation.Execute("1", "1");

        Assert.Equal(expected: "0", result);
    }

    [Fact]
    public void Sub_OneNumberAndOneLetter()
    {
        var operation = new SubOperation();

        Assert.Throws<InvalidOperationException>(() => operation.Execute("1", "a"));
    }
}
