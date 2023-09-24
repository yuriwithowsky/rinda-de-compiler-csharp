namespace RinhaDeCompiladores.Exceptions;

internal class KindNotImplementedExcepton : Exception
{
    public KindNotImplementedExcepton(string kind):
        base($"Kind: {kind} not implemented")
    {
    }
}
