namespace RinhaDeCompiladores.Operations;

public class GtOperation : IOperation
{
    public dynamic Execute(string lhsValue, string rhsValue)
    {
        if (long.TryParse(lhsValue, out long numberLhs) && long.TryParse(rhsValue, out long numberRhs))
        {
            return numberLhs > numberRhs;
        }
        throw new InvalidOperationException($"Invalid op {nameof(GtOperation)} {lhsValue} > {rhsValue}");
    }
}
