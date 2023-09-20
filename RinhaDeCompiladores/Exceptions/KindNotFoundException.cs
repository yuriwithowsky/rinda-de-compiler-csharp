namespace RinhaDeCompiladores.Exceptions;

internal class KindNotFoundException : Exception
{
    public KindNotFoundException(string kind):
        base($"Kind: {kind} not found")
    {
    }
}
