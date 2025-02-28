using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.ListUsers;

public class ListUsersHandler : IRequestHandler<ListUsersCommand, ListUsersResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public ListUsersHandler(
        IUserRepository userRepository,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<ListUsersResult> Handle(ListUsersCommand command, CancellationToken cancellationToken)
    {
        var validator = new ListUsersCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var paginatedResult = await _userRepository.GetAllAsync(command.Page, command.Size, command.Order!, cancellationToken);

        if (paginatedResult == null)
            throw new NotFoundException("No users found");

        var baseUsersResult = _mapper.Map<List<BaseUserResult>>(paginatedResult.Results);

        var result = new ListUsersResult(
            baseUsersResult,
            paginatedResult.CurrentPage,
            paginatedResult.TotalPages,
            paginatedResult.PageSize,
            paginatedResult.TotalCount);

        return result;
    }
}

