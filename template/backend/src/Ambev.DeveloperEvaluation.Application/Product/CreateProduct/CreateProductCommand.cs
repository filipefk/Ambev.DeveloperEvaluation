using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Product.CreateProduct;

public class CreateProductCommand : BaseProductCommand, IRequest<CreateProductResult>
{

}
