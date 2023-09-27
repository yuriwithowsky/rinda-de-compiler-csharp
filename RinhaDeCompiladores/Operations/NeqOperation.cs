namespace RinhaDeCompiladores.Operations;

public class NeqOperation
{
    public string Execute(string lhsValue, string rhsValue)
    {
        return (lhsValue != rhsValue).ToString().ToLower();
    }
}
