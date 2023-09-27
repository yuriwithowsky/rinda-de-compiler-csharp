namespace RinhaDeCompiladores.Operations;

public class EqOperation
{
    public string Execute(string lhsValue, string rhsValue)
    {
        return (lhsValue == rhsValue).ToString().ToLower();
    }
}
