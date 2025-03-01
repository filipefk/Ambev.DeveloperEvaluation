using Ambev.DeveloperEvaluation.Application.Product.UpdateProduct;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Product.TestData;
using Ambev.DeveloperEvaluation.Unit.Application.Products.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products;

public class UpdateProductHandlerTests
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly UpdateProductHandler _handler;

    public UpdateProductHandlerTests()
    {
        _productRepository = Substitute.For<IProductRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<UpdateProductProfile>()));
        _handler = new UpdateProductHandler(_productRepository, _unitOfWork, _mapper);
    }

    [Fact(DisplayName = "Given valid product data When updating product Then returns success response")]
    public async Task Handle_Valid_Command_Returns_Success_Response()
    {
        // Given
        var command = UpdateProductHandlerTestData.GenerateValidCommand();
        var product = ProductTestData.GenerateValidProduct();
        product.Id = command.Id;

        _productRepository.GetByIdAsync(
            command.Id,
            Arg.Any<CancellationToken>())
            .Returns(product);

        // When
        var updateProductResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        updateProductResult.Should().NotBeNull();
        updateProductResult.Id.Should().Be(product.Id);
        updateProductResult.Title.Should().Be(product.Title);
        updateProductResult.Description.Should().Be(product.Description);
        updateProductResult.Price.Should().Be(product.Price);
        updateProductResult.Category.Should().Be(product.Category);
        updateProductResult.Image.Should().Be(product.Image);
        updateProductResult.Rating.Rate.Should().Be(product.Rating.Rate);
        updateProductResult.Rating.Count.Should().Be(product.Rating.Count);

        await _productRepository.Received(1).GetByIdAsync(command.Id, CancellationToken.None);
        await _unitOfWork.Received(1).CommitAsync(Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given invalid product data When updating product Then throws validation exception")]
    public async Task Handle_Invalid_Command_Throws_ValidationException()
    {
        // Given
        var command = new UpdateProductCommand // Empty command will fail validation
        {
            Id = Guid.NewGuid(),
            Rating = new DeveloperEvaluation.Application.Product.BaseRatingApp()
        };

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();

        await _productRepository.Received(0).GetByIdAsync(Arg.Any<Guid>(), CancellationToken.None);
        await _unitOfWork.Received(0).CommitAsync(Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given valid product data When updating product When product not exists Then throws NotFoundException")]
    public async Task Handle_Valid_Command_Product_Not_Exists_Throws_NotFoundException()
    {
        // Given
        var command = UpdateProductHandlerTestData.GenerateValidCommand();

        _productRepository.GetByIdAsync(
            command.Id,
            Arg.Any<CancellationToken>())
            .Returns((DeveloperEvaluation.Domain.Entities.Product)null!);

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<NotFoundException>();

        await _productRepository.Received(1).GetByIdAsync(command.Id, CancellationToken.None);
        await _unitOfWork.Received(0).CommitAsync(Arg.Any<CancellationToken>());
    }

}
