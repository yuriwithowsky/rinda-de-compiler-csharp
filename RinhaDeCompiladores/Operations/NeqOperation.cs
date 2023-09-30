namespace RinhaDeCompiladores.Operations;

public class NeqOperation : IOperation
{
    public dynamic Execute(string lhsValue, string rhsValue)
    {
        return lhsValue != rhsValue;
    }
}
