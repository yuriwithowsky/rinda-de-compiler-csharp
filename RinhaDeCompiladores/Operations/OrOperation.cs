namespace RinhaDeCompiladores.Operations;

public class OrOperation
{
    public string Execute(string lhsValue, string rhsValue)
    {
        if (bool.TryParse(lhsValue, out bool boolLhs) && bool.TryParse(rhsValue, out bool boolRhs))
        {
            return $"{boolLhs || boolRhs}".ToLower();
        }
        throw new InvalidOperationException($"Invalid op {nameof(OrOperation)} {lhsValue} || {rhsValue}");
    }
}
