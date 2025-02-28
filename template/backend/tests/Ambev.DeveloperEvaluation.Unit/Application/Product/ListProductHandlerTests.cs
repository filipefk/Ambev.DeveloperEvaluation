using Ambev.DeveloperEvaluation.Application.Product.ListProducts;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Products.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products;

public class ListProductsHandlerTests
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ListProductsHandler _handler;

    public ListProductsHandlerTests()
    {
        _productRepository = Substitute.For<IProductRepository>();
        _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<ListProductsProfile>()));
        _handler = new ListProductsHandler(_productRepository, _mapper);
    }

    [Fact(DisplayName = "Given valid command When product exists When get products Then returns PaginatedResult Products")]
    public async Task Handle_ValidCommand_Returns_PaginatedResult_Products()
    {
        // Given
        var command = ListProductsHandlerTestData.GenerateValidCommand();
        var totalCount = new Random().Next(1, 20);
        var paginatedResultProducts = ListProductsHandlerTestData.GeneratePaginatedResultProducts(totalCount, command.Page, command.Size);

        // When
        _productRepository.GetAllAsync(
            command.Page,
            command.Size,
            Arg.Any<string>(),
            cancellationToken: CancellationToken.None)
            .Returns(paginatedResultProducts);

        // When
        var listProductsResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        listProductsResult.Should().NotBeNull();
        listProductsResult.TotalCount.Should().Be(totalCount);
        listProductsResult.CurrentPage.Should().Be(command.Page);
        listProductsResult.PageSize.Should().Be(command.Size);
        listProductsResult.TotalPages.Should().Be((int)Math.Ceiling((double)totalCount / command.Size));
        listProductsResult.Products.Count.Should().Be(paginatedResultProducts.Results.Count);

        await _productRepository.Received(1).GetAllAsync(command.Page, command.Size, Arg.Any<string>(), CancellationToken.None);
    }

    [Fact(DisplayName = "Given a invalid command When get products Then throws validation exception")]
    public async Task Handle_InvalidCommand_Returns_Validation_Exception()
    {
        // Given
        var command = ListProductsHandlerTestData.GenerateInvalidCommand();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    [Fact(DisplayName = "Given a valid command When products not exists When get products Then throws a InvalidOperationException")]
    public async Task Handle_ValidCommand_With_ProductID_Not_Found_Returns_KeyNotFoundException_Exception()
    {
        // Given
        var command = ListProductsHandlerTestData.GenerateValidCommand();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<InvalidOperationException>();
    }

}