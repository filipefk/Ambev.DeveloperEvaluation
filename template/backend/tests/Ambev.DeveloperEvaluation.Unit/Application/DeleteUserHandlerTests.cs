using Ambev.DeveloperEvaluation.Application.Users.DeleteUser;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;
public class DeleteUserHandlerTests
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly DeleteUserHandler _handler;

    public DeleteUserHandlerTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _handler = new DeleteUserHandler(_userRepository, _unitOfWork);
    }

    [Fact(DisplayName = "Given valid command When user exists When deleting user Then returns success response")]
    public async Task Handle_ValidCommand_Returns_Success_Response()
    {
        // Given
        var command = DeleteUserHandlerTestData.GenerateValidCommand();

        // When
        _userRepository.DeleteAsync(command.Id, Arg.Any<CancellationToken>())
        .Returns(true);

        var deleteUserResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        deleteUserResult.Should().NotBeNull();
        deleteUserResult.Success.Should().BeTrue();
        await _userRepository.Received(1).DeleteAsync(command.Id, CancellationToken.None);
        await _unitOfWork.Received(1).CommitAsync(Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given a invalid command When deleting user Then throws validation exception")]
    public async Task Handle_InvalidCommand_Returns_Validation_Exception()
    {
        // Given
        var command = DeleteUserHandlerTestData.GenerateInvalidCommand();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
        await _userRepository.Received(0).DeleteAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
        await _unitOfWork.Received(0).CommitAsync(Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given a valid command When user not exists When deleting user Then throws a KeyNotFoundException exception")]
    public async Task Handle_ValidCommand_With_UserID_Not_Found_Returns_KeyNotFoundException_Exception()
    {
        // Given
        var command = DeleteUserHandlerTestData.GenerateValidCommand();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<KeyNotFoundException>();
        await _userRepository.Received(1).DeleteAsync(command.Id, CancellationToken.None);
        await _unitOfWork.Received(1).CommitAsync(Arg.Any<CancellationToken>());
    }
}