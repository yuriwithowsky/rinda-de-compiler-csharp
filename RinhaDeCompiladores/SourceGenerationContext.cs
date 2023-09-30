using RinhaDeCompiladores.Ast;
using System.Text.Json.Serialization;

[JsonSourceGenerationOptions(
    WriteIndented = true,
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(AstRoot))]
[JsonSerializable(typeof(Let))]
[JsonSerializable(typeof(RinhaDeCompiladores.Ast.File))]
[JsonSerializable(typeof(Var))]
[JsonSerializable(typeof(Function))]
[JsonSerializable(typeof(Str))]
[JsonSerializable(typeof(Bool))]
[JsonSerializable(typeof(Int))]
[JsonSerializable(typeof(Binary))]
[JsonSerializable(typeof(Print))]
[JsonSerializable(typeof(Call))]
[JsonSerializable(typeof(If))]
[JsonSerializable(typeof(First))]
[JsonSerializable(typeof(Second))]
[JsonSerializable(typeof(RinhaDeCompiladores.Ast.Tuple))]
public partial class SourceGenerationContext : JsonSerializerContext
{
}