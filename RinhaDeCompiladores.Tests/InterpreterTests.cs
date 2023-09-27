using System.Text.Json.Nodes;

namespace RinhaDeCompiladores.Tests;

public class InterpreterTests
{
    [Fact]
    public void Sum()
    {
        var path = "var/rinha/sum.json";

        using FileStream stream = File.OpenRead(path);
        var root = JsonObject.Parse(stream);
        var expression = root["expression"];

        var interpreter = new Interpreter();

        var result = interpreter.Execute(expression, new Dictionary<string, JsonNode>());

        Assert.NotNull(result);
        Assert.Equal("15", result);
    }

    [Fact]
    public void SimpleSum()
    {
        var path = "var/rinha/primitive-sum.json";

        using FileStream stream = File.OpenRead(path);
        var root = JsonObject.Parse(stream);
        var expression = root["expression"];

        var interpreter = new Interpreter();

        var result = interpreter.Execute(expression, new Dictionary<string, JsonNode>());

        Assert.NotNull(result);
        Assert.Equal("3", result);
    }

    [Fact]
    public void Combination()
    {
        var path = "var/rinha/combination.json";

        using FileStream stream = File.OpenRead(path);
        var root = JsonObject.Parse(stream);
        var expression = root["expression"];

        var interpreter = new Interpreter();

        var result = interpreter.Execute(expression, new Dictionary<string, JsonNode>());

        Assert.NotNull(result);
        Assert.Equal("45", result);
    }

    [Fact]
    public void Fib()
    {
        var path = "var/rinha/fib.json";

        using FileStream stream = File.OpenRead(path);
        var root = JsonObject.Parse(stream);
        var expression = root["expression"];

        var interpreter = new Interpreter();

        var result = interpreter.Execute(expression, new Dictionary<string, JsonNode>());

        Assert.NotNull(result);
        Assert.Equal("832040", result);
    }

    [Fact]
    public void Tuple()
    {
        var path = "var/rinha/tuple.json";

        using FileStream stream = File.OpenRead(path);
        var root = JsonObject.Parse(stream);
        var expression = root["expression"];

        var interpreter = new Interpreter();

        var result = interpreter.Execute(expression, new Dictionary<string, JsonNode>());

        Assert.NotNull(result);
        Assert.Equal("(1,2)", result);
    }

    [Fact]
    public void VariavelLivre()
    {
        var path = "var/rinha/variavel-livre.json";

        using FileStream stream = File.OpenRead(path);
        var root = JsonObject.Parse(stream);
        var expression = root["expression"];

        var interpreter = new Interpreter();

        var result = interpreter.Execute(expression, new Dictionary<string, JsonNode>());

        Assert.NotNull(result);
        Assert.Equal("1", result);
    }

    [Fact]
    public void Closure()
    {
        var path = "var/rinha/closure.json";

        using FileStream stream = File.OpenRead(path);
        var root = JsonObject.Parse(stream);
        var expression = root["expression"];

        var interpreter = new Interpreter();

        var result = interpreter.Execute(expression, new Dictionary<string, JsonNode>());

        Assert.NotNull(result);
        Assert.Equal("30", result);
    }

    [Fact]
    public void PrintClosure()
    {
        var path = "var/rinha/print_closure.json";

        using FileStream stream = File.OpenRead(path);
        var root = JsonObject.Parse(stream);
        var expression = root["expression"];

        var interpreter = new Interpreter();

        var result = interpreter.Execute(expression, new Dictionary<string, JsonNode>());

        Assert.NotNull(result);
        Assert.Equal("1", result);
    }
}