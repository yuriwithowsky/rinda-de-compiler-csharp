namespace RinhaDeCompiladores.Operations;

public class LteOperation : IOperation
{
    public dynamic Execute(string lhsValue, string rhsValue)
    {
        if (int.TryParse(lhsValue, out int numberLhs) && int.TryParse(rhsValue, out int numberRhs))
        {
            return numberLhs <= numberRhs;
        }
        throw new InvalidOperationException($"Invalid op {nameof(LteOperation)} {lhsValue} <= {rhsValue}");
    }
}
