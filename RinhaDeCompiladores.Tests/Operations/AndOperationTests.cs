using RinhaDeCompiladores.Operations;

namespace RinhaDeCompiladores.Tests.Operations;

public class AndOperationTests
{
    [Fact]
    public void And_TrueAndTrue()
    {
        var operation = new AndOperation();

        var result = operation.Execute("true", "true");

        Assert.Equal(expected: "true", result);
    }
    [Fact]
    public void And_FalseAndFalse()
    {
        var operation = new AndOperation();

        var result = operation.Execute("false", "false");

        Assert.Equal(expected: "false", result);
    }

    [Fact]
    public void And_TrueAndFalse()
    {
        var operation = new AndOperation();

        var result = operation.Execute("true", "false");

        Assert.Equal(expected: "false", result);
    }
    [Fact]
    public void And_OneNumberAndOneLetter()
    {
        var operation = new AndOperation();

        Assert.Throws<InvalidOperationException>(() => operation.Execute("1", "0"));
    }
}
