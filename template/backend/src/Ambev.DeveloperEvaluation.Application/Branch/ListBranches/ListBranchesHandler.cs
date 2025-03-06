using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Branch.ListBranches;

public class ListBranchesHandler : IRequestHandler<ListBranchesCommand, ListBranchesResult>
{
    private readonly IBranchRepository _branchRepository;
    private readonly IMapper _mapper;

    public ListBranchesHandler(
        IBranchRepository productRepository,
        IMapper mapper)
    {
        _branchRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ListBranchesResult> Handle(ListBranchesCommand command, CancellationToken cancellationToken)
    {
        var validator = new ListBranchesCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var paginatedResult = await _branchRepository.GetAllAsync(command.Page, command.Size, command.Order!, cancellationToken);

        if (paginatedResult == null)
            throw new NotFoundException("No branches found");

        var baseBranchesResult = _mapper.Map<List<BaseBranchResult>>(paginatedResult.Results);

        var result = new ListBranchesResult(
            baseBranchesResult,
            paginatedResult.CurrentPage,
            paginatedResult.TotalPages,
            paginatedResult.PageSize,
            paginatedResult.TotalCount);

        return result;
    }
}

