namespace RinhaDeCompiladores.Operations;

public class AddOperation : IOperation
{
    public dynamic Execute(string lhsValue, string rhsValue)
    {
        if (long.TryParse(lhsValue, out long numberLhs) && long.TryParse(rhsValue, out long numberRhs))
        {
            return numberLhs + numberRhs;

        }
        return $"{lhsValue + rhsValue}";
    }
}
