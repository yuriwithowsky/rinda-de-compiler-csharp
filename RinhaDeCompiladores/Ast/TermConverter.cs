using System.Text.Json;
using System.Text.Json.Serialization;

namespace RinhaDeCompiladores.Ast;

public class TermConverter : JsonConverter<Term>
{
    public override Term? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument doc = JsonDocument.ParseValue(ref reader);

        var root = doc.RootElement;

        if (root.TryGetProperty("kind", out var kindProperty))
        {
            if (!Enum.TryParse(kindProperty.GetString(), out AstKind kind))
            {
                throw new JsonException($"Kind cannot be parsed to Enum value: {kind}");
            }

            var termType = GetTermTypeByKind(kind);

            return root.Deserialize(termType, options) as Term;
        }

        throw new JsonException("Missing 'kind' property");
    }

    private static Type GetTermTypeByKind(AstKind kind)
    {
        return kind switch
        {
            AstKind.Let => typeof(Let),
            AstKind.Function => typeof(Function),
            AstKind.Binary => typeof(Binary),
            AstKind.Int => typeof(Int),
            AstKind.Var => typeof(Var),
            AstKind.Call => typeof(Call),
            AstKind.If => typeof(If),
            AstKind.Print => typeof(Print),
            AstKind.Tuple => typeof(Tuple),
            AstKind.Bool => typeof(Bool),
            AstKind.Str => typeof(Str),
            AstKind.File => typeof(File),
            AstKind.First => typeof(First),
            AstKind.Second => typeof(Second),
            _ => throw new JsonException($"Unknown 'kind' value: {kind}")
        };
    }

    public override void Write(Utf8JsonWriter writer, Term value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, options);
    }
}
