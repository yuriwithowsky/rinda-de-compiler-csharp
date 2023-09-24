using RinhaDeCompiladores;
using System.Text.Json.Nodes;

//var fileName = "tuple_print.json";
var fileName = "source.rinha.json";

if (args.Length > 0 && args[0] is not null)
    fileName = args[0];

var path = $"var/rinha/{fileName}";

using FileStream stream = File.OpenRead(path);
var root = JsonObject.Parse(stream);
var expression = root["expression"];

var interpreter = new Interpreter();
    
interpreter.Execute(expression, new Dictionary<string, JsonNode>());

Console.ReadKey();