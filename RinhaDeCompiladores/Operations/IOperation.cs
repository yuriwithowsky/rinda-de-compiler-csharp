namespace RinhaDeCompiladores.Operations;

public interface IOperation
{
    public dynamic Execute(string lhsValue, string rhsValue);
}
