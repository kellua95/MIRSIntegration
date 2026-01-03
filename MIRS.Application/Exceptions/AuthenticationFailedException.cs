namespace MIRS.Application.Exceptions;

public sealed class AuthenticationFailedException : ApplicationException
{
    public AuthenticationFailedException(string message)
        : base(message)
    {
    }
}