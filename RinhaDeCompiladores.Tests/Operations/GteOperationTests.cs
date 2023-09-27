using RinhaDeCompiladores.Operations;

namespace RinhaDeCompiladores.Tests.Operations;

public class GteOperationTests
{
    [Fact]
    public void Gte_FirstNumberGreaterThanSecondNumber()
    {
        var operation = new GteOperation();

        var result = operation.Execute("5", "1");

        Assert.Equal(expected: "true", result);
    }
    [Fact]
    public void Gte_FirstNumberIsEqualThanSecondNumber()
    {
        var operation = new GteOperation();

        var result = operation.Execute("5", "5");

        Assert.Equal(expected: "true", result);
    }

    [Fact]
    public void Gte_SecondNumberGreaterThanFirstNumber()
    {
        var operation = new GteOperation();

        var result = operation.Execute("5", "10");

        Assert.Equal(expected: "false", result);
    }

    [Fact]
    public void Gte_OneNumberAndOneLetter()
    {
        var operation = new GteOperation();

        Assert.Throws<InvalidOperationException>(() => operation.Execute("1", "a"));
    }
}
