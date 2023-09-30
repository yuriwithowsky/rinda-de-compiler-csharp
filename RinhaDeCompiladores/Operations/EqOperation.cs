namespace RinhaDeCompiladores.Operations;

public class EqOperation : IOperation
{
    public dynamic Execute(string lhsValue, string rhsValue)
    {
        return lhsValue == rhsValue;
    }
}
