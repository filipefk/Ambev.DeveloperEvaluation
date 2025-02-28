using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Product.UpdateProduct;

public class UpdateProductCommand : BaseProductCommand, IRequest<UpdateProductResult>
{
    public required Guid Id { get; set; }
}
