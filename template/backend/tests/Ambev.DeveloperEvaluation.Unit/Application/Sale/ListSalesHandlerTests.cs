using Ambev.DeveloperEvaluation.Application.Sale.ListSales;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Sale.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sale;

public class ListSalesHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly ListSalesHandler _handler;

    public ListSalesHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<ListSalesProfile>()));
        _handler = new ListSalesHandler(_saleRepository, _mapper);
    }

    [Fact(DisplayName = "Given valid command When sale exists When get sales Then returns PaginatedResult Sales")]
    public async Task Handle_ValidCommand_Returns_PaginatedResult_Sales()
    {
        // Given
        var command = ListSalesHandlerTestData.GenerateValidCommand();
        var totalCount = new Random().Next(1, 20);
        var paginatedResultSales = ListSalesHandlerTestData.GeneratePaginatedResultSales(totalCount, command.Page, command.Size);

        // When
        _saleRepository.GetAllAsync(
            command.Page,
            command.Size,
            Arg.Any<string>(),
            cancellationToken: CancellationToken.None)
            .Returns(paginatedResultSales);

        // When
        var listSalesResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        listSalesResult.Should().NotBeNull();
        listSalesResult.TotalCount.Should().Be(totalCount);
        listSalesResult.CurrentPage.Should().Be(command.Page);
        listSalesResult.PageSize.Should().Be(command.Size);
        listSalesResult.TotalPages.Should().Be((int)Math.Ceiling((double)totalCount / command.Size));

        await _saleRepository.Received(1).GetAllAsync(command.Page, command.Size, Arg.Any<string>(), CancellationToken.None);
    }

    [Fact(DisplayName = "Given a invalid command When get sales Then throws validation exception")]
    public async Task Handle_InvalidCommand_Returns_Validation_Exception()
    {
        // Given
        var command = ListSalesHandlerTestData.GenerateInvalidCommand();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    [Fact(DisplayName = "Given a valid command When sales not exists When get sales Then throws a NotFoundException")]
    public async Task Handle_ValidCommand_With_SaleID_Not_Found_Returns_NotFoundException()
    {
        // Given
        var command = ListSalesHandlerTestData.GenerateValidCommand();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<NotFoundException>();
    }

}