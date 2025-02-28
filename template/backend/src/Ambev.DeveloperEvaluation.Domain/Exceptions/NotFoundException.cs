using System.Net;

namespace Ambev.DeveloperEvaluation.Domain.Exceptions;

public class NotFoundException : ExceptionBase
{
    public NotFoundException(string message) : base(message) { }

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.NotFound;
}
