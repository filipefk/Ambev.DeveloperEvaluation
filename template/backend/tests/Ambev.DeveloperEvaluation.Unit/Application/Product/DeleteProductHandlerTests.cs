using Ambev.DeveloperEvaluation.Application.Product.DeleteProduct;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Product.TestData;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products;
public class DeleteProductHandlerTests
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly DeleteProductHandler _handler;

    public DeleteProductHandlerTests()
    {
        _productRepository = Substitute.For<IProductRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _handler = new DeleteProductHandler(_productRepository, _unitOfWork);
    }

    [Fact(DisplayName = "Given valid command When product exists When deleting product Then returns success response")]
    public async Task Handle_ValidCommand_Returns_Success_Response()
    {
        // Given
        var command = DeleteProductHandlerTestData.GenerateValidCommand();

        // When
        _productRepository.DeleteAsync(command.Id, Arg.Any<CancellationToken>())
        .Returns(true);

        var deleteProductResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        deleteProductResult.Should().NotBeNull();
        deleteProductResult.Success.Should().BeTrue();
        await _productRepository.Received(1).DeleteAsync(command.Id, CancellationToken.None);
        await _unitOfWork.Received(1).CommitAsync(Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given a invalid command When deleting product Then throws validation exception")]
    public async Task Handle_InvalidCommand_Returns_Validation_Exception()
    {
        // Given
        var command = DeleteProductHandlerTestData.GenerateInvalidCommand();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
        await _productRepository.Received(0).DeleteAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
        await _unitOfWork.Received(0).CommitAsync(Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given a valid command When product not exists When deleting product Then throws a KeyNotFoundException exception")]
    public async Task Handle_ValidCommand_With_ProductID_Not_Found_Returns_KeyNotFoundException_Exception()
    {
        // Given
        var command = DeleteProductHandlerTestData.GenerateValidCommand();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<KeyNotFoundException>();
        await _productRepository.Received(1).DeleteAsync(command.Id, CancellationToken.None);
        await _unitOfWork.Received(1).CommitAsync(Arg.Any<CancellationToken>());
    }
}