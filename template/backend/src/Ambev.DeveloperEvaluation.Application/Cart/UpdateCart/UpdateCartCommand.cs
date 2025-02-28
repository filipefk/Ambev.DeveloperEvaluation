using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Cart.UpdateCart;

public class UpdateCartCommand : BaseCartCommand, IRequest<UpdateCartResult>
{
    public required Guid Id { get; set; }
}
