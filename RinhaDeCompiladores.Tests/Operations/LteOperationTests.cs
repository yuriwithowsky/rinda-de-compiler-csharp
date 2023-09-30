using RinhaDeCompiladores.Operations;

namespace RinhaDeCompiladores.Tests.Operations;

public class LteOperationTests
{
    [Fact]
    public void Lte_FirstNumberLessThanSecondNumber()
    {
        var operation = new LteOperation();

        var result = operation.Execute("1", "5");

        Assert.True(result);
    }
    [Fact]
    public void Lte_FirstNumberIsEqualThanSecondNumber()
    {
        var operation = new LteOperation();

        var result = operation.Execute("5", "5");

        Assert.True(result);
    }

    [Fact]
    public void Lte_SecondNumberLessThanFirstNumber()
    {
        var operation = new LteOperation();

        var result = operation.Execute("5", "1");

        Assert.False(result);
    }

    [Fact]
    public void Lte_OneNumberAndOneLetter()
    {
        var operation = new LteOperation();

        Assert.Throws<InvalidOperationException>(() => operation.Execute("1", "a"));
    }
}
