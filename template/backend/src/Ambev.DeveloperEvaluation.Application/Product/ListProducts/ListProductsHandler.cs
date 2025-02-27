using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Product.ListProducts;

public class ListProductsHandler : IRequestHandler<ListProductsCommand, ListProductsResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ListProductsHandler(
        IProductRepository productRepository,
        IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ListProductsResult> Handle(ListProductsCommand command, CancellationToken cancellationToken)
    {
        var validator = new ListProductsCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var paginatedResult = await _productRepository.GetAllAsync(command.Page, command.Size, command.Order!, cancellationToken);

        if (paginatedResult == null)
            throw new InvalidOperationException("No products found");

        var baseProductsResult = _mapper.Map<List<BaseProductResult>>(paginatedResult.Results);

        var result = new ListProductsResult(
            baseProductsResult,
            paginatedResult.CurrentPage,
            paginatedResult.TotalPages,
            paginatedResult.PageSize,
            paginatedResult.TotalCount);

        return result;
    }
}

