using Ambev.DeveloperEvaluation.Application.Cart.GetCart;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Cart.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Cart;

public class GetCartHandlerTests
{
    private readonly ICartRepository _cartRepository;
    private readonly IMapper _mapper;
    private readonly GetCartHandler _handler;

    public GetCartHandlerTests()
    {
        _cartRepository = Substitute.For<ICartRepository>();
        _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<GetCartProfile>()));
        _handler = new GetCartHandler(_cartRepository, _mapper);
    }

    [Fact(DisplayName = "Given valid command When cart exists When get cart Then returns cart")]
    public async Task Handle_ValidCommand_Returns_Cart()
    {
        // Given
        var command = GetCartHandlerTestData.GenerateValidCommand();
        var cart = CartTestData.GenerateValidCart();
        cart.Id = command.Id;

        // When
        _cartRepository.GetByIdAsync(
            command.Id,
            Arg.Any<CancellationToken>())
            .Returns(cart);

        // When
        var getCartResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        getCartResult.Should().NotBeNull();
        getCartResult.Id.Should().Be(cart.Id);
        getCartResult.UserId.Should().Be(cart.UserId);
        getCartResult.Date.Should().Be(cart.Date);
        getCartResult.Products.Count.Should().Be(cart.Products.Count);
        for (int i = 0; i < cart.Products.Count; i++)
        {
            var expectedProduct = cart.Products.ElementAt(i);
            var actualProduct = getCartResult.Products.ElementAt(i);

            actualProduct.ProductId.Should().Be(expectedProduct.ProductId);
            actualProduct.Quantity.Should().Be(expectedProduct.Quantity);
        }

        await _cartRepository.Received(1).GetByIdAsync(command.Id, CancellationToken.None);
    }

    [Fact(DisplayName = "Given a invalid command When get cart Then throws validation exception")]
    public async Task Handle_InvalidCommand_Returns_Validation_Exception()
    {
        // Given
        var command = GetCartHandlerTestData.GenerateInvalidCommand();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    [Fact(DisplayName = "Given a valid command When cart not exists When get cart Then throws a KeyNotFoundException exception")]
    public async Task Handle_ValidCommand_With_CartID_Not_Found_Returns_KeyNotFoundException_Exception()
    {
        // Given
        var command = GetCartHandlerTestData.GenerateValidCommand();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<KeyNotFoundException>();
    }
}