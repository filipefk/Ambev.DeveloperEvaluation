using System.Net;

namespace Ambev.DeveloperEvaluation.Domain.Exceptions;

public class InvalidLoginException : ExceptionBase
{
    public InvalidLoginException() : base("Email or password invalid") { }

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
}
