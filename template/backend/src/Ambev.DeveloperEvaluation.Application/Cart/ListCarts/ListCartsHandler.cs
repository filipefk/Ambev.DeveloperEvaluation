using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Cart.ListCarts;

public class ListCartsHandler : IRequestHandler<ListCartsCommand, ListCartsResult>
{
    private readonly ICartRepository _productRepository;
    private readonly IMapper _mapper;

    public ListCartsHandler(
        ICartRepository productRepository,
        IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ListCartsResult> Handle(ListCartsCommand command, CancellationToken cancellationToken)
    {
        var validator = new ListCartsCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var paginatedResult = await _productRepository.GetAllAsync(command.Page, command.Size, command.Order!, cancellationToken);

        if (paginatedResult == null)
            throw new InvalidOperationException("No products found");

        var baseCartsResult = _mapper.Map<List<BaseCartResult>>(paginatedResult.Results);

        var result = new ListCartsResult(
            baseCartsResult,
            paginatedResult.CurrentPage,
            paginatedResult.TotalPages,
            paginatedResult.PageSize,
            paginatedResult.TotalCount);

        return result;
    }
}

