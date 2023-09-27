using RinhaDeCompiladores.Operations;

namespace RinhaDeCompiladores.Tests.Operations;

public class OrOperationTests
{
    [Fact]
    public void Or_TrueAndTrue()
    {
        var operation = new OrOperation();

        var result = operation.Execute("true", "true");

        Assert.Equal(expected: "true", result);
    }
    [Fact]
    public void Or_FalseAndFalse()
    {
        var operation = new OrOperation();

        var result = operation.Execute("false", "false");

        Assert.Equal(expected: "false", result);
    }

    [Fact]
    public void Or_TrueAndFalse()
    {
        var operation = new OrOperation();

        var result = operation.Execute("true", "false");

        Assert.Equal(expected: "true", result);
    }

    [Fact]
    public void Or_FalseAndTrue()
    {
        var operation = new OrOperation();

        var result = operation.Execute("false", "true");

        Assert.Equal(expected: "true", result);
    }

    [Fact]
    public void Or_OneNumberOrOneLetter()
    {
        var operation = new OrOperation();

        Assert.Throws<InvalidOperationException>(() => operation.Execute("1", "0"));
    }
}