using RinhaDeCompiladores;
using System.Diagnostics;
using System.Text.Json.Nodes;

var fileName = "source.rinha.json";

if (args.Length > 0 && args[0] is not null)
    fileName = args[0];

var path = $"/var/rinha/{fileName}";

#if DEBUG
    var stopwatch = new Stopwatch();
    stopwatch.Start();
    path = "var/rinha/ops.json";
#endif

using FileStream stream = File.OpenRead(path);
var root = JsonObject.Parse(stream);
var expression = root["expression"];

var interpreter = new Interpreter();
    
interpreter.Execute(expression, new Dictionary<string, JsonNode>());

#if DEBUG
    stopwatch.Stop();
    Console.WriteLine($"Finalizado em {stopwatch.Elapsed}");
#endif