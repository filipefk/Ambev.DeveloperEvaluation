using Ambev.DeveloperEvaluation.Application.Cart.UpdateCart;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Cart.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Cart;

public class UpdateCartHandlerTests
{
    private readonly ICartRepository _cartRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly UpdateCartHandler _handler;

    public UpdateCartHandlerTests()
    {
        _cartRepository = Substitute.For<ICartRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<UpdateCartProfile>()));
        _handler = new UpdateCartHandler(_cartRepository, _unitOfWork, _mapper);
    }

    [Fact(DisplayName = "Given valid cart data When updating cart Then returns success response")]
    public async Task Handle_Valid_Command_Returns_Success_Response()
    {
        // Given
        var command = UpdateCartHandlerTestData.GenerateValidCommand();
        var cart = CartTestData.GenerateValidCart();
        cart.Id = command.Id;

        _cartRepository.GetByIdAsync(
            command.Id,
            Arg.Any<CancellationToken>())
            .Returns(cart);

        // When
        var updateCartResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        updateCartResult.Should().NotBeNull();
        updateCartResult.Id.Should().Be(command.Id);
        updateCartResult.UserId.Should().Be(cart.UserId);
        updateCartResult.Date.Should().Be(cart.Date);
        updateCartResult.Products.Count.Should().Be(cart.Products.Count);
        for (int i = 0; i < cart.Products.Count; i++)
        {
            var expectedProduct = cart.Products.ElementAt(i);
            var actualProduct = updateCartResult.Products.ElementAt(i);

            actualProduct.ProductId.Should().Be(expectedProduct.ProductId);
            actualProduct.Quantity.Should().Be(expectedProduct.Quantity);
        }

        await _cartRepository.Received(1).GetByIdAsync(command.Id, CancellationToken.None);
        await _unitOfWork.Received(1).CommitAsync(Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given invalid cart data When updating cart Then throws validation exception")]
    public async Task Handle_Invalid_Command_Throws_ValidationException()
    {
        // Given
        var command = new UpdateCartCommand() // Empty command will fail validation
        {
            Id = Guid.NewGuid()
        };
        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();

        await _cartRepository.Received(0).GetByUserAsync(Arg.Any<Guid>(), CancellationToken.None);
        await _cartRepository.Received(0).GetByIdAsync(Arg.Any<Guid>(), CancellationToken.None);
        await _unitOfWork.Received(0).CommitAsync(Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given valid cart data When updating cart When cart not exists Then throws InvalidOperationException")]
    public async Task Handle_Valid_Command_Cart_Not_Exists_Throws_InvalidOperationException()
    {
        // Given
        var command = UpdateCartHandlerTestData.GenerateValidCommand();

        _cartRepository.GetByIdAsync(
            command.Id,
            Arg.Any<CancellationToken>())
            .Returns((DeveloperEvaluation.Domain.Entities.Cart)null!);

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<InvalidOperationException>();

        await _cartRepository.Received(1).GetByIdAsync(command.Id, CancellationToken.None);
        await _unitOfWork.Received(0).CommitAsync(Arg.Any<CancellationToken>());
    }
}
