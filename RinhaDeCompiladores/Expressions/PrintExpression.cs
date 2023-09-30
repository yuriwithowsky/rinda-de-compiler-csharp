using System.Text.Json.Nodes;
using static System.Formats.Asn1.AsnWriter;

namespace RinhaDeCompiladores.Expressions;

public class PrintExpression
{
    public string Execute(JsonNode nodeExpression, Dictionary<string, JsonNode> memory)
    {
        var value = nodeExpression["value"];
        var content = Execute(value, memory);

        Console.Write($"{content}\n");

        return content;
    }
}
