using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Cart.CreateCart;

public class CreateCartCommand : BaseCartCommand, IRequest<CreateCartResult>
{

}
