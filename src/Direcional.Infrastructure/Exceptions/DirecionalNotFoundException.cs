namespace Direcional.Infrastructure.Exceptions;

public class DirecionalNotFoundException : Exception
{
    public DirecionalNotFoundException() : base()
    {

    }

    public DirecionalNotFoundException(string message) : base(message)
    {

    }

    public DirecionalNotFoundException(string message, Exception inner) : base(message, inner)
    {

    }
}