using RinhaDeCompiladores;
using System.Text.Json.Nodes;

try
{
    var fileName = "source.json";

    if (args.Length > 0 && args[0] is not null)
        fileName = args[0];

    var path = $"var/rinha/{fileName}";

    using FileStream stream = File.OpenRead(path);
    var root = JsonObject.Parse(stream);
    var expression = root["expression"];

    var interpreter = new Interpreter();
    
    var result = interpreter.Execute(expression, new Dictionary<string, JsonNode>());
    Console.WriteLine(result);
}
catch (Exception ex)
{
	Console.WriteLine(ex.Message);
}

Console.ReadKey();