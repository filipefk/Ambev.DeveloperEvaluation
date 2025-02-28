using Ambev.DeveloperEvaluation.Application.Sale.GetSale;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Sale.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sale;

public class GetSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly GetSaleHandler _handler;

    public GetSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<GetSaleProfile>()));
        _handler = new GetSaleHandler(_saleRepository, _mapper);
    }

    [Fact(DisplayName = "Given valid command When sale exists When get sale Then returns sale")]
    public async Task Handle_ValidCommand_Returns_Sale()
    {
        // Given
        var command = GetSaleHandlerTestData.GenerateValidCommand();
        var sale = SaleTestData.GenerateValidSale();
        sale.Id = command.Id;

        // When
        _saleRepository.GetByIdAsync(
            command.Id,
            Arg.Any<CancellationToken>())
            .Returns(sale);

        // When
        var getSaleResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        getSaleResult.Should().NotBeNull();
        getSaleResult.Id.Should().Be(sale.Id);
        
        await _saleRepository.Received(1).GetByIdAsync(command.Id, CancellationToken.None);
    }

    [Fact(DisplayName = "Given a invalid command When get sale Then throws validation exception")]
    public async Task Handle_InvalidCommand_Returns_Validation_Exception()
    {
        // Given
        var command = GetSaleHandlerTestData.GenerateInvalidCommand();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    [Fact(DisplayName = "Given a valid command When sale not exists When get sale Then throws a NotFoundException exception")]
    public async Task Handle_ValidCommand_With_SaleID_Not_Found_Returns_NotFoundException_Exception()
    {
        // Given
        var command = GetSaleHandlerTestData.GenerateValidCommand();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<NotFoundException>();
    }
}