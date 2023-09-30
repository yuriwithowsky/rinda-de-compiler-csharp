using RinhaDeCompiladores.Ast;
using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace RinhaDeCompiladores.Tests;

public class InterpreterTests
{
    private Interpreter _interpreter;

    public InterpreterTests()
    {
        _interpreter = new Interpreter();
    }

    private AstRoot Serializer(string path)
    {
        using FileStream stream = System.IO.File.OpenRead(path);
        var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        options.Converters.Add(new TermConverter());
        options.Converters.Add(new JsonStringEnumConverter());

        return JsonSerializer.Deserialize(stream, typeof(AstRoot), new SourceGenerationContext(options)) as AstRoot;
    }

    [Fact]
    public void Sum()
    {
        var path = "var/rinha/sum.json";
        var ast = Serializer(path);

        var result = _interpreter.Execute(ast.Expression, new Dictionary<string, dynamic>());

        Assert.NotNull(result);
        Assert.Equal("15", result);
    }

    [Fact]
    public void SimpleSum()
    {
        var path = "var/rinha/primitive-sum.json";
        var ast = Serializer(path);

        var result = _interpreter.Execute(ast.Expression, new Dictionary<string, dynamic>());

        Assert.NotNull(result);
        Assert.Equal(3, result);
    }

    [Fact]
    public void Combination()
    {
        var path = "var/rinha/combination.json";

        var ast = Serializer(path);

        var result = _interpreter.Execute(ast.Expression, new Dictionary<string, dynamic>());

        Assert.NotNull(result);
        Assert.Equal("45", result);
    }

    [Fact]
    public void Fib()
    {
        var path = "var/rinha/fib.json";
        var ast = Serializer(path);

        var result = _interpreter.Execute(ast.Expression, new Dictionary<string, dynamic>());

        Assert.NotNull(result);
        Assert.Equal("832040", result);
    }

    [Fact]
    public void Tuple()
    {
        var path = "var/rinha/tuple.json";
        var ast = Serializer(path);

        var result = _interpreter.Execute(ast.Expression, new Dictionary<string, dynamic>());

        Assert.NotNull(result);
        Assert.Equal("(1,2)", result);
    }

    [Fact]
    public void VariavelLivre()
    {
        var path = "var/rinha/variavel-livre.json";
        var ast = Serializer(path);

        var result = _interpreter.Execute(ast.Expression, new Dictionary<string, dynamic>());

        Assert.NotNull(result);
        Assert.Equal("1", result);
    }

    [Fact]
    public void Closure()
    {
        var path = "var/rinha/closure.json";
        var ast = Serializer(path);

        var result = _interpreter.Execute(ast.Expression, new Dictionary<string, dynamic>());

        Assert.NotNull(result);
        Assert.Equal("30", result);
    }

    [Fact]
    public void PrintClosure()
    {
        var path = "var/rinha/print_closure.json";
        var ast = Serializer(path);

        var result = _interpreter.Execute(ast.Expression, new Dictionary<string, dynamic>());

        Assert.NotNull(result);
        Assert.Equal("<#closure>", result);
    }
}