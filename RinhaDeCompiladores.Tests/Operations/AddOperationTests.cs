using RinhaDeCompiladores.Operations;

namespace RinhaDeCompiladores.Tests.Operations;

public class AddOperationTests
{
    [Fact]
    public void Add_TwoNumbers()
    {
        var operation = new AddOperation();

        var result = operation.Execute("1", "1");

        Assert.Equal(expected: "2", result);
    }

    [Fact]
    public void Add_OneNumberAndOneLetter()
    {
        var operation = new AddOperation();

        var result = operation.Execute("1", "a");

        Assert.Equal(expected: "1a", result);
    }
}
