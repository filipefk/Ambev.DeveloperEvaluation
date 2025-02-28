using Ambev.DeveloperEvaluation.Application.Sale.DeleteSale;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Sale.TestData;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sale;
public class DeleteSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly DeleteSaleHandler _handler;

    public DeleteSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _handler = new DeleteSaleHandler(_saleRepository, _unitOfWork);
    }

    [Fact(DisplayName = "Given valid command When sale exists When deleting sale Then returns success response")]
    public async Task Handle_ValidCommand_Returns_Success_Response()
    {
        // Given
        var command = DeleteSaleHandlerTestData.GenerateValidCommand();

        // When
        _saleRepository.DeleteAsync(command.Id, Arg.Any<CancellationToken>())
        .Returns(true);

        var deleteSaleResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        deleteSaleResult.Should().NotBeNull();
        deleteSaleResult.Success.Should().BeTrue();
        await _saleRepository.Received(1).DeleteAsync(command.Id, CancellationToken.None);
        await _unitOfWork.Received(1).CommitAsync(Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given a invalid command When deleting sale Then throws validation exception")]
    public async Task Handle_InvalidCommand_Returns_Validation_Exception()
    {
        // Given
        var command = DeleteSaleHandlerTestData.GenerateInvalidCommand();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
        await _saleRepository.Received(0).DeleteAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
        await _unitOfWork.Received(0).CommitAsync(Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given a valid command When sale not exists When deleting sale Then throws a NotFoundException exception")]
    public async Task Handle_ValidCommand_With_SaleID_Not_Found_Returns_NotFoundException_Exception()
    {
        // Given
        var command = DeleteSaleHandlerTestData.GenerateValidCommand();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<NotFoundException>();
        await _saleRepository.Received(1).DeleteAsync(command.Id, CancellationToken.None);
        await _unitOfWork.Received(1).CommitAsync(Arg.Any<CancellationToken>());
    }
}