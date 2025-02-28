using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Ambev.DeveloperEvaluation.Application.Cart.CreateCart;

/// <summary>
/// Handler for processing CreateCartCommand requests
/// </summary>
public class CreateCartHandler : IRequestHandler<CreateCartCommand, CreateCartResult>
{
    private readonly ICartRepository _cartRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Initializes a new instance of CreateCartHandler
    /// </summary>
    /// <param name="cartRepository">The cart repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public CreateCartHandler(
        ICartRepository cartRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IConfiguration configuration)
    {
        _cartRepository = cartRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _configuration = configuration;
    }

    /// <summary>
    /// Handles the CreateCartCommand request
    /// </summary>
    /// <param name="command">The CreateCart command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created cart details</returns>
    public async Task<CreateCartResult> Handle(CreateCartCommand command, CancellationToken cancellationToken)
    {
        var cartMaximumQuantityPerProduct = _configuration.GetValue<int?>("Settings:CartMaximumQuantityPerProduct");
        if (!cartMaximumQuantityPerProduct.HasValue || cartMaximumQuantityPerProduct.Value < 0)
            cartMaximumQuantityPerProduct = 0;

        var validator = new CreateCartCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        CreateCartResult result;

        var existingCart = await _cartRepository.GetByUserAsync(command.UserId, cancellationToken);
        if (existingCart != null)
        {
            foreach (var product in command.Products)
            {
                existingCart.AddProduct(_mapper.Map<Domain.Entities.CartProduct>(product));
            }

            if (existingCart.ExceedsMaximumQuantityPerProduct(cartMaximumQuantityPerProduct.Value))
                throw new OperationInvalidException($"Maximum limit: {cartMaximumQuantityPerProduct} items per product");

            existingCart.UpdatedAt = DateTime.UtcNow;
            result = _mapper.Map<CreateCartResult>(existingCart);
        }
        else
        {
            var newCart = _mapper.Map<Domain.Entities.Cart>(command);

            newCart.ConsolidateProducts();

            if (newCart.ExceedsMaximumQuantityPerProduct(cartMaximumQuantityPerProduct.Value))
                throw new OperationInvalidException($"Maximum limit: {cartMaximumQuantityPerProduct} items per product");

            newCart = await _cartRepository.CreateAsync(newCart, cancellationToken);

            result = _mapper.Map<CreateCartResult>(newCart);
        }

        await _unitOfWork.CommitAsync(cancellationToken);

        return result;
    }

}