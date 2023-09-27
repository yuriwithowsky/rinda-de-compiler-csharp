using RinhaDeCompiladores.Enums;
using RinhaDeCompiladores.Exceptions;
using RinhaDeCompiladores.Operations;
using System.Text.Json.Nodes;

namespace RinhaDeCompiladores;

public class Interpreter
{
    private Dictionary<string, string> _cache = new();
    private Dictionary<string, int> _countCheckPure = new();

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

            return value.ToString().ToLower();
        }
        if (kind.Equals("Int"))
        {
            var value = node["value"];

            return value.ToString();
        }
        if (kind.Equals("Tuple"))
        {
            var first = Execute(node["first"], scope);
            var second = Execute(node["second"], scope);

            return $"({first},{second})";
        }
        if (kind.Equals("Var"))
        {
            var text = node["text"].GetValue<string>();

            if (!scope.ContainsKey(text))
            {
                scope.Add(text, null);
            }
            var newNode = scope[text];

            if (newNode is not JsonObject)
            {
                return newNode.ToString();
            }

            return Execute(newNode, scope);
        }
        if (kind.Equals("Print"))
        {
            var value = node["value"];
            var content = Execute(value, scope);

            Console.Write($"{content}\n");

            return content;
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
                //scope.Add(text, value);
                if (value["kind"].GetValue<string>() == "Function")
                {
                    scope.Add(text, value);
                }
                else
                {
                    scope.Add(text, Execute(value, scope));
                }
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

            if (resultCondition.Equals("True", StringComparison.OrdinalIgnoreCase))
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

            if (newNode is not JsonObject)
            {
                return newNode.ToString();
            }

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

            var key = text + "_" + string.Join(",", localScope.Where(x => x.Value is not JsonObject).Select(x => $"{x.Key}_{x.Value}"));
            
            return ExecuteMemoized(key, newNode, localScope);
        }
        
        throw new KindNotImplementedExcepton(kind);
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

        return ExecuteBinaryOperation(op, lhsValue, rhsValue);
    }

    public string ExecuteBinaryOperation(string operation, string lhsValue, string rhsValue)
    {
        if(!Enum.TryParse(operation, ignoreCase: true, out BinaryOp op))
        {
            throw new Exception("Op not implemented");
        }

        return op switch
        {
            BinaryOp.Add => new AddOperation().Execute(lhsValue, rhsValue),
            BinaryOp.Sub => new SubOperation().Execute(lhsValue, rhsValue),
            BinaryOp.Div => new DivOperation().Execute(lhsValue, rhsValue),
            BinaryOp.Mul => new MulOperation().Execute(lhsValue, rhsValue),
            BinaryOp.Rem => new RemOperation().Execute(lhsValue, rhsValue),
            BinaryOp.Eq =>  new EqOperation().Execute(lhsValue, rhsValue),
            BinaryOp.Neq => new NeqOperation().Execute(lhsValue, rhsValue),
            BinaryOp.Lt =>  new LtOperation().Execute(lhsValue, rhsValue),
            BinaryOp.Gt =>  new GtOperation().Execute(lhsValue, rhsValue),
            BinaryOp.Lte => new LteOperation().Execute(lhsValue, rhsValue),
            BinaryOp.Gte => new GteOperation().Execute(lhsValue, rhsValue),
            BinaryOp.And => new AndOperation().Execute(lhsValue, rhsValue),
            BinaryOp.Or =>  new OrOperation().Execute(lhsValue, rhsValue),
            _           =>  ""
        };
    }

    public string ExecuteMemoized(string key, JsonNode node, Dictionary<string, JsonNode> scope)
    {
        if (!_cache.TryGetValue(key, out string result))
        {
            result = Execute(node, scope);
            _cache[key] = result;
            _countCheckPure[key] = 1;
        } else
        {
            var countCheckPure = _countCheckPure.ContainsKey(key) ? _countCheckPure[key] : 0;
            if (countCheckPure == 1)
            {
                result = Execute(node, scope);
                if(_cache[key] == result)
                {
                    _countCheckPure[key] += 1;
                }
            }
        }

        return result;
    }

}
