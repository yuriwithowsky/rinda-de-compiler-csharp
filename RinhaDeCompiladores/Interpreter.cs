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
        if(term is Ast.File file)
        {
            return Execute(file.Expression, scope);
        }
        if (term is Ast.Str str)
        {
            return str.Value;
        }
        if (term is Ast.Bool @bool)
        {
            return @bool.Value;
        }
        if (term is Ast.Int @int)
        {
            return @int.Value;
        }
        if (term is Ast.Tuple tuple)
        {
            var first = Execute(tuple.First, scope);
            var second = Execute(tuple.Second, scope);

            return $"({first},{second})";
        }
        if (term is Ast.Var var)
        {
            var text = var.Text;

            if (!scope.TryGetValue(text, out dynamic? value))
            {
                throw new Exception($"Var {text} not found");
            }

            return value;
        }
        if (term is Ast.Print print)
        {
            var value = Execute(print.Value, scope);
            var text = string.Empty;

            if(value is Func<List<dynamic>, dynamic>)
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
        if (term is Binary binary)
        {
            return ExecuteBinary(binary, scope);
        }
        if (term is Let let)
        {
            var text = let.Name.Text;
            var value = let.Value;

            scope[text] = Execute(value, scope);

            var localScope = new Dictionary<string, dynamic>();

            foreach (var item in scope)
            {
                localScope.Add(item.Key, item.Value);
            }

            var next = let.Next;

            return Execute(next, localScope);
        }
        if (term is Ast.Function function)
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
        if (term is Ast.If @if)
        {
            var condition = @if.Condition;
            var resultCondition = Execute(condition, scope);

            if (resultCondition == true)
            {
                return Execute(@if.Then, scope);
            }
            return Execute(@if.Otherwise, scope);
        }
        if (term is Call call)
        {
            var callee = call.Callee;

            var arguments =new List<dynamic>();
            string calleeText = null;

            if(callee is Var @var1)
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
        
        throw new KindNotImplementedExcepton(term.GetType().FullName);
    }

    public dynamic ExecuteBinary(Binary binary, Dictionary<string, dynamic> scope)
    {
        var op = binary.Op;

        var lhsValue = Execute(binary.Lhs, scope).ToString();
        var rhsValue = Execute(binary.Rhs, scope).ToString();

        return ExecuteBinaryOperation(op, lhsValue, rhsValue);
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
            _           =>  ""
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
