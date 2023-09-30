using RinhaDeCompiladores.Operations;

namespace RinhaDeCompiladores.Tests.Operations;

public class NeqOperationTests
{
    [Fact]
    public void Neq_FirstValueIsEqualSecond()
    {
        var operation = new NeqOperation();

        var result = operation.Execute("true", "true");

        Assert.False(result);
    }

    [Fact]
    public void Neq_FirstValueIsNotEqualSecond()
    {
        var operation = new NeqOperation();

        var result = operation.Execute("true", "false");

        Assert.True(result);
    }
}
