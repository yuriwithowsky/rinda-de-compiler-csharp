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
        if (kind.Equals("Bool"))
        {
            var value = node["value"].GetValue<bool>();

            return value.ToString();
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
            if (!scope.ContainsKey(text))
            {
                scope.Add(text, value);
            }

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
            var localScope = new Dictionary<string, JsonNode>();

            foreach (var item in scope)
            {
                localScope.Add(item.Key, item.Value);
            }

            var parameters = newNode["parameters"] as JsonArray;
            
            for (int i = 0; i < parameters.Count; i++)
            {
                var argValue = Execute(arguments[i], scope);
                var paramName = parameters[i]["text"].GetValue<string>();

                if(!localScope.ContainsKey(paramName))
                {
                    localScope.Add(parameters[i]["text"].GetValue<string>(), argValue);
                } 
                else
                {
                    localScope[paramName] = argValue;
                }
            }

            return Execute(newNode, localScope);
        }
        
        throw new KindNotFoundException(kind);
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
            throw new InvalidOperationException($"Invalid op {op} {lhsValue} - {rhsValue}");
        }
        if (op.Equals("Div"))
        {
            if (int.TryParse(lhsValue, out int numberLhs) && int.TryParse(rhsValue, out int numberRhs))
            {
                return $"{numberLhs / numberRhs}";

            }
            throw new InvalidOperationException($"Invalid op {op} {lhsValue} / {rhsValue}");
        }
        if (op.Equals("Mul"))
        {
            if (int.TryParse(lhsValue, out int numberLhs) && int.TryParse(rhsValue, out int numberRhs))
            {
                return $"{numberLhs * numberRhs}";

            }
            throw new InvalidOperationException($"Invalid op {op} {lhsValue} * {rhsValue}");
        }
        if (op.Equals("Rem"))
        {
            if (int.TryParse(lhsValue, out int numberLhs) && int.TryParse(rhsValue, out int numberRhs))
            {
                return $"{numberLhs % numberRhs}";

            }
            throw new InvalidOperationException($"Invalid op {op} {lhsValue} % {rhsValue}");
        }
        if (op.Equals("Lt"))
        {
            if (int.TryParse(lhsValue, out int numberLhs) && int.TryParse(rhsValue, out int numberRhs))
            {
                return $"{numberLhs < numberRhs}";

            }
            throw new InvalidOperationException($"Invalid op {op} {lhsValue} < {rhsValue}");
        }
        if (op.Equals("Lte"))
        {
            if (int.TryParse(lhsValue, out int numberLhs) && int.TryParse(rhsValue, out int numberRhs))
            {
                return $"{numberLhs <= numberRhs}";

            }
            throw new InvalidOperationException($"Invalid op {op} {lhsValue} <= {rhsValue}");
        }
        if (op.Equals("Eq"))
        {
            return (lhsValue == rhsValue).ToString();
        }
        if (op.Equals("Neq"))
        {
            return (lhsValue != rhsValue).ToString();
        }
        if (op.Equals("Gt"))
        {
            if (int.TryParse(lhsValue, out int numberLhs) && int.TryParse(rhsValue, out int numberRhs))
            {
                return $"{numberLhs > numberRhs}";

            }
            throw new InvalidOperationException($"Invalid op {op} {lhsValue} > {rhsValue}");
        }
        if (op.Equals("Gte"))
        {
            if (int.TryParse(lhsValue, out int numberLhs) && int.TryParse(rhsValue, out int numberRhs))
            {
                return $"{numberLhs >= numberRhs}";

            }
            throw new InvalidOperationException($"Invalid op {op} {lhsValue} >= {rhsValue}");
        }
        if (op.Equals("And"))
        {
            if (bool.TryParse(lhsValue, out bool boolLhs) && bool.TryParse(rhsValue, out bool boolRhs))
            {
                return $"{boolLhs && boolRhs}";

            }
            throw new InvalidOperationException($"Invalid op {op} {lhsValue} && {rhsValue}");
        }
        if (op.Equals("Or"))
        {
            if (bool.TryParse(lhsValue, out bool boolLhs) && bool.TryParse(rhsValue, out bool boolRhs))
            {
                return $"{boolLhs || boolRhs}";

            }
            throw new InvalidOperationException($"Invalid op {op} {lhsValue} && {rhsValue}");
        }

        return null;
    }
}
