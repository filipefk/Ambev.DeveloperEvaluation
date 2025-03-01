using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sale.ListSales;

public class ListSalesHandler : IRequestHandler<ListSalesCommand, ListSalesResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    public ListSalesHandler(
        ISaleRepository saleRepository,
        IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    public async Task<ListSalesResult> Handle(ListSalesCommand command, CancellationToken cancellationToken)
    {
        var validator = new ListSalesCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var paginatedResult = await _saleRepository.GetAllAsync(command.Page, command.Size, command.Order!, cancellationToken);

        if (paginatedResult == null)
            throw new NotFoundException("No sales found");

        var baseSalesResult = _mapper.Map<List<BaseSaleResult>>(paginatedResult.Results);

        var result = new ListSalesResult(
            baseSalesResult,
            paginatedResult.CurrentPage,
            paginatedResult.TotalPages,
            paginatedResult.PageSize,
            paginatedResult.TotalCount);

        return result;
    }
}

