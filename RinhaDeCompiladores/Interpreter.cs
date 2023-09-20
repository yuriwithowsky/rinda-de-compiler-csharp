using RinhaDeCompiladores.Exceptions;
using System.Text.Json.Nodes;

namespace RinhaDeCompiladores;

public class Interpreter
{
    public string Execute(JsonNode node, Dictionary<string, JsonNode> scope)
    {
        var kind = node["kind"].GetValue<string>();

        if (kind.Equals("Str"))
        {
            var value = node["value"].GetValue<string>();

            return value;
        }
        if (kind.Equals("Int"))
        {
            var value = node["value"];

            return value.ToString();
        }
        if (kind.Equals("Var"))
        {
            var text = node["text"].GetValue<string>();

            var _scope = scope[text];

            if(_scope is not JsonObject)
            {
                return _scope.ToString();
            }

            return Execute(_scope, scope);
        }
        if (kind.Equals("Print"))
        {
            var value = node["value"];

            return Execute(value, scope);
        }
        if (kind.Equals("Binary"))
        {
            return ExecuteBinary(node, scope);
        }
        if (kind.Equals("Let"))
        {
            var text = node["name"]["text"].ToString();
            var value = node["value"];
            scope.Add(text, value);

            var next = node["next"];

            return Execute(next, scope);
        }
        if (kind.Equals("Function"))
        {
            return Execute(node["value"], scope);
        }
        if (kind.Equals("If"))
        {
            var condition = node["condition"];
            var resultCondition = Execute(condition, scope);

            if (resultCondition.Equals("True"))
            {
                return Execute(node["then"], scope);
            }
            return Execute(node["otherwise"], scope);
        }
        if (kind.Equals("Call"))
        {
            var callee = node["callee"];
            var arguments = node["arguments"];

            var text = callee["text"].GetValue<string>();

            var newNode = scope[text];

            var parameters = newNode["parameters"] as JsonArray;
            var newScope = new Dictionary<string, JsonNode>();
            
            for (int i = 0; i < parameters.Count; i++)
            {
                var argValue = Execute(arguments[i], scope);
                var paramName = parameters[i]["text"].GetValue<string>();

                if(!scope.ContainsKey(paramName))
                {
                    scope.Add(parameters[i]["text"].GetValue<string>(), argValue);
                } else
                {
                    scope[paramName] = argValue;
                }

            }

            return Execute(newNode, scope);
            //Execute(conditional, scope);
        }
        
        throw new KindNotFoundException(kind);
        
        return null;
        //if (expression.Kind.Equals("Binary"))
        //{
        //    ExecuteBinary(expression);
        //}

        //if (expression.Kind.Equals("Let"))
        //{
        //    var functionName = expression.Name.Text;
        //    Console.WriteLine(functionName);

        //    ExecuteLet(expression.Value);
        //}

        //if (expression.Kind.Equals("Let"))
        //{
        //    var functionName = expression.Name.Text;
        //    Console.WriteLine(functionName);

        //    Execute(expression.Next);
        //}

    }

    public void ExecutePrint(JsonNode node)
    {
        var value = node["value"].GetValue<string>();
        Console.WriteLine(value);
    }

    public string ExecuteBinary(JsonNode node, Dictionary<string, JsonNode> scope)
    {
        var op = node["op"].GetValue<string>();

        var lhsValue = Execute(node["lhs"], scope);
        var rhsValue = Execute(node["rhs"], scope);

        if (op.Equals("Add"))
        {
            if (int.TryParse(lhsValue, out int numberLhs) && int.TryParse(rhsValue, out int numberRhs))
            {
                return $"{numberLhs + numberRhs}";

            }
            return $"{lhsValue + rhsValue}";
        }
        if (op.Equals("Sub"))
        {
            if (int.TryParse(lhsValue, out int numberLhs) && int.TryParse(rhsValue, out int numberRhs))
            {
                return $"{numberLhs - numberRhs}";

            }
            return $"{lhsValue + rhsValue}";
        }
        if (op.Equals("Lt"))
        {
            if (int.TryParse(lhsValue, out int numberLhs) && int.TryParse(rhsValue, out int numberRhs))
            {
                return $"{numberLhs < numberRhs}";

            }
            return $"{lhsValue + rhsValue}";
        }
        if (op.Equals("Eq"))
        {
            return (lhsValue == rhsValue).ToString();
        }

        return null;
    }
}
