namespace RinhaDeCompiladores.Operations;

public class LtOperation
{
    public string Execute(string lhsValue, string rhsValue)
    {
        if (int.TryParse(lhsValue, out int numberLhs) && int.TryParse(rhsValue, out int numberRhs))
        {
            return $"{numberLhs < numberRhs}".ToLower();
        }
        throw new InvalidOperationException($"Invalid op {nameof(LtOperation)} {lhsValue} < {rhsValue}");
    }
}
