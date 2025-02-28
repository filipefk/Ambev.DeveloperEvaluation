using Ambev.DeveloperEvaluation.Application.Cart.DeleteCart;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Cart.TestData;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Cart;
public class DeleteCartHandlerTests
{
    private readonly ICartRepository _cartRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly DeleteCartHandler _handler;

    public DeleteCartHandlerTests()
    {
        _cartRepository = Substitute.For<ICartRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _handler = new DeleteCartHandler(_cartRepository, _unitOfWork);
    }

    [Fact(DisplayName = "Given valid command When cart exists When deleting cart Then returns success response")]
    public async Task Handle_ValidCommand_Returns_Success_Response()
    {
        // Given
        var command = DeleteCartHandlerTestData.GenerateValidCommand();

        // When
        _cartRepository.DeleteAsync(command.Id, Arg.Any<CancellationToken>())
        .Returns(true);

        var deleteCartResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        deleteCartResult.Should().NotBeNull();
        deleteCartResult.Success.Should().BeTrue();
        await _cartRepository.Received(1).DeleteAsync(command.Id, CancellationToken.None);
        await _unitOfWork.Received(1).CommitAsync(Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given a invalid command When deleting cart Then throws validation exception")]
    public async Task Handle_InvalidCommand_Returns_Validation_Exception()
    {
        // Given
        var command = DeleteCartHandlerTestData.GenerateInvalidCommand();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
        await _cartRepository.Received(0).DeleteAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
        await _unitOfWork.Received(0).CommitAsync(Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given a valid command When cart not exists When deleting cart Then throws a KeyNotFoundException exception")]
    public async Task Handle_ValidCommand_With_CartID_Not_Found_Returns_KeyNotFoundException_Exception()
    {
        // Given
        var command = DeleteCartHandlerTestData.GenerateValidCommand();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<KeyNotFoundException>();
        await _cartRepository.Received(1).DeleteAsync(command.Id, CancellationToken.None);
        await _unitOfWork.Received(1).CommitAsync(Arg.Any<CancellationToken>());
    }
}