using System.Net;

namespace Ambev.DeveloperEvaluation.Domain.Exceptions;

public abstract class ExceptionBase : SystemException
{
    public ExceptionBase(string message) : base(message) { }

    public abstract HttpStatusCode GetStatusCode();
}
