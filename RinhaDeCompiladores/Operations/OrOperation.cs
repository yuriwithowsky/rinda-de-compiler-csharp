namespace RinhaDeCompiladores.Operations;

public class OrOperation : IOperation
{
    public dynamic Execute(string lhsValue, string rhsValue)
    {
        if (bool.TryParse(lhsValue, out bool boolLhs) && bool.TryParse(rhsValue, out bool boolRhs))
        {
            return boolLhs || boolRhs;
        }
        throw new InvalidOperationException($"Invalid op {nameof(OrOperation)} {lhsValue} || {rhsValue}");
    }
}
