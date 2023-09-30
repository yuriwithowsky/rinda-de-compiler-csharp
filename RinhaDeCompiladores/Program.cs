using RinhaDeCompiladores;
using RinhaDeCompiladores.Ast;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

var fileName = "source.rinha.json";

if (args.Length > 0 && args[0] is not null)
    fileName = args[0];

var path = $"/var/rinha/{fileName}";

#if DEBUG
    var stopwatch = new Stopwatch();
    stopwatch.Start();
    path = "var/rinha/operations.json";
#endif

using FileStream stream = System.IO.File.OpenRead(path);

var interpreter = new Interpreter();

var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
options.Converters.Add(new TermConverter());
options.Converters.Add(new JsonStringEnumConverter());

var deserialized = JsonSerializer.Deserialize(stream, typeof(AstRoot), new SourceGenerationContext(options)) as AstRoot;

interpreter.Execute(deserialized.Expression, new Dictionary<string, dynamic>());

#if DEBUG
    stopwatch.Stop();
    Console.WriteLine($"Finalizado em {stopwatch.Elapsed}");
#endif