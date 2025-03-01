using Ambev.DeveloperEvaluation.Application.Product.GetProduct;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Product.TestData;
using Ambev.DeveloperEvaluation.Unit.Application.Products.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products;

public class GetProductHandlerTests
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly GetProductHandler _handler;

    public GetProductHandlerTests()
    {
        _productRepository = Substitute.For<IProductRepository>();
        _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<GetProductProfile>()));
        _handler = new GetProductHandler(_productRepository, _mapper);
    }

    [Fact(DisplayName = "Given valid command When product exists When get product Then returns product")]
    public async Task Handle_ValidCommand_Returns_Product()
    {
        // Given
        var command = GetProductHandlerTestData.GenerateValidCommand();
        var product = ProductTestData.GenerateValidProduct();
        product.Id = command.Id;

        // When
        _productRepository.GetByIdAsync(
            command.Id,
            Arg.Any<CancellationToken>())
            .Returns(product);

        // When
        var getProductResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        getProductResult.Should().NotBeNull();
        getProductResult.Id.Should().Be(product.Id);
        getProductResult.Title.Should().Be(product.Title);
        getProductResult.Description.Should().Be(product.Description);
        getProductResult.Price.Should().Be(product.Price);
        getProductResult.Category.Should().Be(product.Category);
        getProductResult.Image.Should().Be(product.Image);
        getProductResult.Rating.Rate.Should().Be(product.Rating.Rate);
        getProductResult.Rating.Count.Should().Be(product.Rating.Count);

        await _productRepository.Received(1).GetByIdAsync(command.Id, CancellationToken.None);
    }

    [Fact(DisplayName = "Given a invalid command When get product Then throws validation exception")]
    public async Task Handle_InvalidCommand_Returns_Validation_Exception()
    {
        // Given
        var command = GetProductHandlerTestData.GenerateInvalidCommand();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    [Fact(DisplayName = "Given a valid command When product not exists When get product Then throws a NotFoundException")]
    public async Task Handle_ValidCommand_With_ProductID_Not_Found_Returns_NotFoundException()
    {
        // Given
        var command = GetProductHandlerTestData.GenerateValidCommand();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<NotFoundException>();
    }
}