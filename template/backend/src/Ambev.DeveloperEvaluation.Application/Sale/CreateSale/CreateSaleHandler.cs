using Ambev.DeveloperEvaluation.Application.Sale.Notification;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sale.CreateSale;

/// <summary>
/// Handler for processing CreateSaleCommand requests
/// </summary>
public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly ICartRepository _cartRepository;
    private readonly IBranchRepository _branchRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMediator _bus;

    /// <summary>
    /// Initializes a new instance of CreateSaleHandler
    /// </summary>
    /// <param name="saleRepository">The sale repository</param>
    /// <param name="cartRepository">The cart repository</param>
    /// <param name="branchRepository">The branch repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public CreateSaleHandler(
        ISaleRepository saleRepository,
        ICartRepository cartRepository,
        IBranchRepository branchRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IMediator bus)
    {
        _saleRepository = saleRepository;
        _cartRepository = cartRepository;
        _branchRepository = branchRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _bus = bus;
    }

    /// <summary>
    /// Handles the CreateSaleCommand request
    /// </summary>
    /// <param name="command">The CreateSale command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created sale details</returns>
    public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var branch = await _branchRepository.GetByIdAsync(command.BranchId, cancellationToken);
        if (branch == null)
            throw new NotFoundException($"Branch with ID {command.BranchId} not found");

        var cart = await _cartRepository.GetByIdAsync(command.CartId, cancellationToken);
        if (cart == null)
            throw new NotFoundException($"Cart with ID {command.CartId} not found");

        var sale = Domain.Entities.Sale.SaleFactory(cart, branch);

        // TODO - Make the discount rules configurable
        sale.ApplyDiscounts();

        sale = await _saleRepository.CreateAsync(sale, cancellationToken);

        await _cartRepository.DeleteAsync(cart.Id, cancellationToken);

        var result = _mapper.Map<CreateSaleResult>(sale);

        await _unitOfWork.CommitAsync(cancellationToken);

        var saleCreatedNotification = new SaleCreatedNotification(sale);
        await _bus.Publish(saleCreatedNotification, cancellationToken);

        return result;
    }

}