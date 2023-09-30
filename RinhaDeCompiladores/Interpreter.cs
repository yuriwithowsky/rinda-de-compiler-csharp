using RinhaDeCompiladores.Ast;
using RinhaDeCompiladores.Enums;
using RinhaDeCompiladores.Exceptions;
using RinhaDeCompiladores.Operations;

namespace RinhaDeCompiladores;

public class Interpreter
{
    private Dictionary<string, dynamic> _cache = new();
    private Dictionary<string, int> _countCheckPure = new();

    public dynamic Execute(Term term, Dictionary<string, dynamic> scope)
    {
        return term.Kind switch { 
            AstKind.Bool => ExecuteBool(term as Bool),
            AstKind.Str => ExecuteStr(term as Ast.Str),
            AstKind.Int => ExecuteInt(term as Ast.Int),
            AstKind.File => ExecuteFile(term as Ast.File, scope),
            AstKind.Tuple => ExecuteTuple(term as Ast.Tuple, scope),
            AstKind.First => ExecuteFirst(term as Ast.First, scope),
            AstKind.Second => ExecuteSecond(term as Ast.Second, scope),
            AstKind.Var => ExecuteVar(term as Ast.Var, scope),
            AstKind.Print => ExecutePrint(term as Ast.Print, scope),
            AstKind.Binary => ExecuteBinary(term as Ast.Binary, scope),
            AstKind.Let => ExecuteLet(term as Ast.Let, scope),
            AstKind.Function => ExecuteFunction(term as Ast.Function, scope),
            AstKind.If => ExecuteIf(term as Ast.If, scope),
            AstKind.Call => ExecuteCall(term as Ast.Call, scope),
            _ => throw new KindNotImplementedExcepton(term.GetType().FullName)
        };
    }

    public dynamic ExecuteFile(Ast.File file, Dictionary<string, object> scope) => Execute(file.Expression, scope);

    public static string ExecuteStr(Ast.Str str) => str.Value;

    public static bool ExecuteBool(Ast.Bool @bool) => @bool.Value;
    
    public static int ExecuteInt(Ast.Int @int) => @int.Value;

    public dynamic[] ExecuteTuple(Ast.Tuple tuple, Dictionary<string, object> scope)
    {
        var firstValue = Execute(tuple.First, scope);
        var secondValue = Execute(tuple.Second, scope);

        return new dynamic[] { firstValue, secondValue };
    }

    public dynamic ExecuteFirst(First first, Dictionary<string, object> scope)
    {
        var value = Execute(first.Value, scope);

        if (value is dynamic[] array)
        {
            return array[0];
        }
        throw new Exception("First needs a Tuple");
    }

    public dynamic ExecuteSecond(Second second, Dictionary<string, object> scope)
    {
        var value = Execute(second.Value, scope);

        if (value is dynamic[] array)
        {
            return array[1];
        }
        throw new Exception("Second needs a Tuple");
    }

    public dynamic ExecuteVar(Var var, Dictionary<string, object> scope)
    {
        var text = var.Text;

        if (!scope.TryGetValue(text, out dynamic? value))
        {
            throw new Exception($"Var {text} not found");
        }

        if (value is Var)
        {
            return Execute(value, scope);
        }

        return value;
    }
    
    public dynamic ExecuteFunction(Function function, Dictionary<string, object> scope)
    {
        var func = new Func<List<dynamic>, dynamic>((arguments) => {
            var parameters = function.Parameters;

            var localScope = new Dictionary<string, dynamic>();

            foreach (var item in scope)
            {
                localScope.Add(item.Key, item.Value);
            }

            for (int i = 0; i < arguments.Count; i++)
            {
                var paramName = parameters[i].Text;
                localScope[paramName] = arguments[i];
            }

            return Execute(function.Value, localScope);
        });

        return func;
    }

    public dynamic ExecuteCall(Call call, Dictionary<string, object> scope)
    {
        var callee = call.Callee;

        var arguments = new List<dynamic>();
        string calleeText = null;

        if (callee is Var @var1)
        {
            calleeText = @var1.Text;
        }

        foreach (var item in call.Arguments)
        {
            var argValue = Execute(item, scope);
            arguments.Add(argValue);
        }

        var key = calleeText + "_" + string.Join(",", arguments.Select(x => $"{x}"));

        var result = ExecuteMemoized(key, callee, scope, arguments);

        return result;
    }

    public dynamic ExecuteIf(If @if, Dictionary<string, object> scope)
    {
        var condition = @if.Condition;
        var resultCondition = Execute(condition, scope);

        if (resultCondition == true)
        {
            return Execute(@if.Then, scope);
        }
        return Execute(@if.Otherwise, scope);
    }

    public dynamic ExecutePrint(Print print, Dictionary<string, dynamic> scope)
    {
        var value = Execute(print.Value, scope);
        
        var text = string.Empty;

        if (value is dynamic[] array)
        {
            text = $"({array[0]},{array[1]})";
        }
        else if (value is Func<List<dynamic>, dynamic>)
        {
            text = $"<#closure>";
        }
        else
        {
            text = value.ToString();
        }

        Console.Write($"{text}\n");

        return text;
    }

    public dynamic ExecuteLet(Let let, Dictionary<string, dynamic> scope)
    {
        var text = let.Name.Text;
        var value = let.Value;

        if (value is Var)
        {
            scope[text] = value;
        }
        else
        {
            scope[text] = Execute(value, scope);
        }

        return Execute(let.Next, scope);
    }

    public dynamic ExecuteBinary(Binary binary, Dictionary<string, dynamic> scope)
    {
        var lhsValue = Execute(binary.Lhs, scope).ToString();
        var rhsValue = Execute(binary.Rhs, scope).ToString();

        return ExecuteBinaryOperation(binary.Op, lhsValue, rhsValue);
    }

    public dynamic ExecuteBinaryOperation(BinaryOp op, string lhsValue, string rhsValue)
    {
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
            _ =>  throw new NotImplementedException($"BinaryOp not implemented value: {op}")
        };
    }

    public dynamic ExecuteMemoized(string key, Term term, Dictionary<string, dynamic> scope, List<dynamic> arguments)
    {
        if (!_cache.TryGetValue(key, out dynamic result))
        {
            result = Execute(term, scope)(arguments);
            _cache[key] = result;
            _countCheckPure[key] = 1;
        }
        else
        {
            var countCheckPure = _countCheckPure.ContainsKey(key) ? _countCheckPure[key] : 0;
            if (countCheckPure == 1)
            {
                result = Execute(term, scope)(arguments);
                if (_cache[key] == result)
                {
                    _countCheckPure[key] += 1;
                }
            }
        }

        return result;
    }

}
