using System.Net;

namespace Ambev.DeveloperEvaluation.Domain.Exceptions;

public class UnauthorizedException : ExceptionBase
{
    public UnauthorizedException(string message) : base(message) { }

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
}
