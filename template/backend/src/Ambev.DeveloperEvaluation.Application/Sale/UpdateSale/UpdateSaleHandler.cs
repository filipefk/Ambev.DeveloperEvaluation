using Ambev.DeveloperEvaluation.Application.Sale.Notification;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sale.UpdateSale;

public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IBranchRepository _branchRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMediator _bus;

    /// <summary>
    /// Initializes a new instance of CreateSaleHandler
    /// </summary>
    /// <param name="saleRepository">The sale repository</param>
    /// <param name="branchRepository">The branch repository</param>
    /// <param name="unitOfWork">The UnitOfWork instance</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public UpdateSaleHandler(
        ISaleRepository saleRepository,
        IBranchRepository branchRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IMediator bus)
    {
        _saleRepository = saleRepository;
        _branchRepository = branchRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _bus = bus;
    }

    /// <summary>
    /// Handles the UpdateSaleCommand request
    /// </summary>
    /// <param name="command">The UpdateSale command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The update sale details</returns>
    public async Task<UpdateSaleResult> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
    {
        

        var validator = new UpdateSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var sale = await _saleRepository.GetByIdAsync(command.Id, cancellationToken);
        if (sale == null)
            throw new NotFoundException($"Sale with id {command.Id} does not exists");

        var branch = await _branchRepository.GetByIdAsync(command.BranchId, cancellationToken);
        if (branch == null)
            throw new NotFoundException($"Branch with ID {command.BranchId} not found");

        var user = await _userRepository.GetByIdAsync(command.UserId, cancellationToken);
        if (user == null)
            throw new NotFoundException($"User with ID {command.UserId} not found");

        var isCanceledBefore = sale.Canceled;

        _mapper.Map(command, sale);

        sale.ApplyDiscounts();

        sale.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.CommitAsync(cancellationToken);

        var saleModifiedNotification = new SaleModifiedNotification(sale);
        await _bus.Publish(saleModifiedNotification, cancellationToken);

        if (!isCanceledBefore && sale.Canceled)
        {
            var saleCancelledNotification = new SaleCancelledNotification(sale);
            await _bus.Publish(saleCancelledNotification, cancellationToken);
        }

        var result = _mapper.Map<UpdateSaleResult>(sale);
        return result;
    }
}
