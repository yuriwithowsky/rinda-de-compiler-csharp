using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace RinhaDeCompiladores.Ast;

public class TermConverter : JsonConverter<Term>
{
    public override Term Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
        {
            var root = doc.RootElement;
            if (root.TryGetProperty("kind", out var kindProperty))
            {
                string kind = kindProperty.GetString();
                switch (kind)
                {
                    case "Let":
                        return JsonSerializer.Deserialize<Let>(root.GetRawText(), options);
                    case "Function":
                        return JsonSerializer.Deserialize<Function>(root.GetRawText(), options);
                    case "Binary":
                        return JsonSerializer.Deserialize<Binary>(root.GetRawText(), options);
                    case "Int":
                        return JsonSerializer.Deserialize<Int>(root.GetRawText(), options);
                    case "Var":
                        return JsonSerializer.Deserialize<Var>(root.GetRawText(), options);
                    case "Call":
                        return JsonSerializer.Deserialize<Call>(root.GetRawText(), options);
                    case "If":
                        return JsonSerializer.Deserialize<If>(root.GetRawText(), options);
                    case "Print":
                        return JsonSerializer.Deserialize<Print>(root.GetRawText(), options);
                    case "Tuple":
                        return JsonSerializer.Deserialize<Tuple>(root.GetRawText(), options);
                    case "Bool":
                        return JsonSerializer.Deserialize<Bool>(root.GetRawText(), options);
                    case "Str":
                        return JsonSerializer.Deserialize<Str>(root.GetRawText(), options);
                    case "File":
                        return JsonSerializer.Deserialize<File>(root.GetRawText(), options);
                    case "First":
                        return JsonSerializer.Deserialize<First>(root.GetRawText(), options);
                    case "Second":
                        return JsonSerializer.Deserialize<Second>(root.GetRawText(), options);
                    // Adicione outros casos para os tipos de Term necessários
                    default:
                        throw new JsonException($"Unknown 'kind' value: {kind}");
                }
            }
            throw new JsonException("Missing 'kind' property");
        }
    }

    public override void Write(Utf8JsonWriter writer, Term value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, options);
    }
}
