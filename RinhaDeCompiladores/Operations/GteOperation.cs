namespace RinhaDeCompiladores.Operations;

public class GteOperation : IOperation
{
    public dynamic Execute(string lhsValue, string rhsValue)
    {
        if (int.TryParse(lhsValue, out int numberLhs) && int.TryParse(rhsValue, out int numberRhs))
        {
            return numberLhs >= numberRhs;
        }
        throw new InvalidOperationException($"Invalid op {nameof(GteOperation)} {lhsValue} >= {rhsValue}");
    }
}
