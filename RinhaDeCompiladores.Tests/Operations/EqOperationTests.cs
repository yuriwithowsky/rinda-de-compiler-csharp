using RinhaDeCompiladores.Operations;

namespace RinhaDeCompiladores.Tests.Operations;

public class EqOperationTests
{
    [Fact]
    public void Eq_FirstValueIsEqualSecond()
    {
        var operation = new EqOperation();

        var result = operation.Execute("true", "true");

        Assert.True(result);
    }

    [Fact]
    public void Eq_FirstValueIsNotEqualSecond()
    {
        var operation = new EqOperation();

        var result = operation.Execute("true", "false");

        Assert.False(result);
    }
}
