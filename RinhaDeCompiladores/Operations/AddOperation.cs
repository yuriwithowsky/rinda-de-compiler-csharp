namespace RinhaDeCompiladores.Operations;

public class AddOperation
{
    public string Execute(string lhsValue, string rhsValue)
    {
        if (int.TryParse(lhsValue, out int numberLhs) && int.TryParse(rhsValue, out int numberRhs))
        {
            return $"{numberLhs + numberRhs}";

        }
        return $"{lhsValue + rhsValue}";
    }
}
