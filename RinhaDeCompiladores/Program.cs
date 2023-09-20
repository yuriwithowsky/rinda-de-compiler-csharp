using RinhaDeCompiladores;
using System.Text.Json;
using System.Text.Json.Nodes;

try
{
    //var path = "C:\\Users\\yuriw\\source\\repos\\RinhaDeCompiladores\\print.json";
    //var path = "C:\\Users\\yuriw\\source\\repos\\RinhaDeCompiladores\\primitive-sum.json";
    //var path = "C:\\Users\\yuriw\\source\\repos\\RinhaDeCompiladores\\sum.json";
    //var path = "C:\\Users\\yuriw\\source\\repos\\RinhaDeCompiladores\\print_let.json";
    var path = "C:\\Users\\yuriw\\source\\repos\\RinhaDeCompiladores\\fib.json";

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