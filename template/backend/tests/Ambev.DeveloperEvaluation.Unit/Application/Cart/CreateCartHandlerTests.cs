using Ambev.DeveloperEvaluation.Application.Cart;
using Ambev.DeveloperEvaluation.Application.Cart.CreateCart;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Cart.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Cart;

/// <summary>
/// Contains unit tests for the <see cref="CreateCartHandler"/> class.
/// </summary>
public class CreateCartHandlerTests
{
    private readonly ICartRepository _cartRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly CreateCartHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateCartHandlerTests"/> class.
    /// Sets up the test dependencies and creates fake data generators.
    /// </summary>
    public CreateCartHandlerTests()
    {
        _cartRepository = Substitute.For<ICartRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<CreateCartProfile>()));
        _handler = new CreateCartHandler(_cartRepository, _unitOfWork, _mapper);
    }

    /// <summary>
    /// Tests that a valid cart creation request is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid cart data When creating cart Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var command = CreateCartHandlerTestData.GenerateValidCommand();
        var cart = new DeveloperEvaluation.Domain.Entities.Cart
        {
            Id = Guid.NewGuid(),
            Date = command.Date,
            User = null!,
            Products = command.Products.Select(p => new CartProduct
            {
                ProductId = p.ProductId,
                Quantity = p.Quantity,
                Product = null!,
                Cart = null!
            }).ToList(),
        };

        var result = new CreateCartResult
        {
            Id = cart.Id,
            Date = command.Date,
            Products = command.Products.Select(p => new BaseCartProductApp
            {
                ProductId = p.ProductId,
                Quantity = p.Quantity,
            }).ToList(),
        };


        _cartRepository.CreateAsync(Arg.Any<DeveloperEvaluation.Domain.Entities.Cart>(), Arg.Any<CancellationToken>())
            .Returns(cart);

        // When
        var createCartResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        createCartResult.Should().NotBeNull();
        createCartResult.Id.Should().Be(cart.Id);

        await _cartRepository.Received(1).CreateAsync(Arg.Any<DeveloperEvaluation.Domain.Entities.Cart>(), Arg.Any<CancellationToken>());
        await _unitOfWork.Received(1).CommitAsync(Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that an invalid cart creation request throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid cart data When creating cart Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new CreateCartCommand(); // Empty command will fail validation

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();

        await _cartRepository.Received(0).CreateAsync(Arg.Any<DeveloperEvaluation.Domain.Entities.Cart>(), Arg.Any<CancellationToken>());
        await _unitOfWork.Received(0).CommitAsync(Arg.Any<CancellationToken>());
    }

}
