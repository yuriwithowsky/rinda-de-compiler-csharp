namespace RinhaDeCompiladores.Operations;

public class GteOperation
{
    public string Execute(string lhsValue, string rhsValue)
    {
        if (int.TryParse(lhsValue, out int numberLhs) && int.TryParse(rhsValue, out int numberRhs))
        {
            return $"{numberLhs >= numberRhs}".ToLower();
        }
        throw new InvalidOperationException($"Invalid op {nameof(GteOperation)} {lhsValue} >= {rhsValue}");
    }
}
