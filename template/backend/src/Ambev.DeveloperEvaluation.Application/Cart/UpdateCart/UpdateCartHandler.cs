using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Ambev.DeveloperEvaluation.Application.Cart.UpdateCart;

public class UpdateCartHandler : IRequestHandler<UpdateCartCommand, UpdateCartResult>
{
    private readonly ICartRepository _cartRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Initializes a new instance of CreateCartHandler
    /// </summary>
    /// <param name="cartRepository">The cart repository</param>
    /// <param name="unitOfWork">The UnitOfWork instance</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public UpdateCartHandler(
        ICartRepository cartRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IConfiguration configuration)
    {
        _cartRepository = cartRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _configuration = configuration;
    }

    /// <summary>
    /// Handles the UpdateCartCommand request
    /// </summary>
    /// <param name="command">The UpdateCart command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The update cart details</returns>
    public async Task<UpdateCartResult> Handle(UpdateCartCommand command, CancellationToken cancellationToken)
    {
        var cartMaximumQuantityPerProduct = _configuration.GetValue<int?>("Settings:CartMaximumQuantityPerProduct");
        if (!cartMaximumQuantityPerProduct.HasValue || cartMaximumQuantityPerProduct.Value < 0)
            cartMaximumQuantityPerProduct = 0;

        var validator = new UpdateCartCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var cart = await _cartRepository.GetByIdAsync(command.Id, cancellationToken);
        if (cart == null)
            throw new NotFoundException($"Cart with id {command.Id} does not exists");

        _mapper.Map(command, cart);

        if (cart.ExceedsMaximumQuantityPerProduct(cartMaximumQuantityPerProduct.Value))
            throw new OperationInvalidException($"Maximum limit: {cartMaximumQuantityPerProduct} items per product");

        cart.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.CommitAsync(cancellationToken);

        var result = _mapper.Map<UpdateCartResult>(cart);
        return result;
    }
}
