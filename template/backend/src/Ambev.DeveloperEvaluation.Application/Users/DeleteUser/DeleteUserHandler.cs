using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.DeleteUser;

/// <summary>
/// Handler for processing DeleteUserCommand requests
/// </summary>
public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, DeleteUserResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of DeleteUserHandler
    /// </summary>
    /// <param name="userRepository">The user repository</param>
    /// <param name="validator">The validator for DeleteUserCommand</param>
    public DeleteUserHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Handles the DeleteUserCommand request
    /// </summary>
    /// <param name="command">The DeleteUser command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The result of the delete operation</returns>
    public async Task<DeleteUserResult> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        var validator = new DeleteUserCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var success = await _userRepository.DeleteAsync(command.Id, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        if (!success)
            throw new KeyNotFoundException($"User with ID {command.Id} not found");

        return new DeleteUserResult { Success = true };
    }
}
