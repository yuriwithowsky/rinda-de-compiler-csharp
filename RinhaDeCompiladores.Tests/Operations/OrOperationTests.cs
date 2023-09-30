using RinhaDeCompiladores.Operations;

namespace RinhaDeCompiladores.Tests.Operations;

public class OrOperationTests
{
    [Fact]
    public void Or_TrueAndTrue()
    {
        var operation = new OrOperation();

        var result = operation.Execute("true", "true");

        Assert.True(result);
    }
    [Fact]
    public void Or_FalseAndFalse()
    {
        var operation = new OrOperation();

        var result = operation.Execute("false", "false");

        Assert.False(result);
    }

    [Fact]
    public void Or_TrueAndFalse()
    {
        var operation = new OrOperation();

        var result = operation.Execute("true", "false");

        Assert.True(result);
    }

    [Fact]
    public void Or_FalseAndTrue()
    {
        var operation = new OrOperation();

        var result = operation.Execute("false", "true");

        Assert.True(result);
    }

    [Fact]
    public void Or_OneNumberOrOneLetter()
    {
        var operation = new OrOperation();

        Assert.Throws<InvalidOperationException>(() => operation.Execute("1", "0"));
    }
}