using System.Net;

namespace Ambev.DeveloperEvaluation.Domain.Exceptions;

public class OperationInvalidException : ExceptionBase
{
    public OperationInvalidException(string message) : base(message) { }

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
}
