using Ambev.DeveloperEvaluation.Application.Cart.ListCarts;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Cart.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Cart;

public class ListCartsHandlerTests
{
    private readonly ICartRepository _cartRepository;
    private readonly IMapper _mapper;
    private readonly ListCartsHandler _handler;

    public ListCartsHandlerTests()
    {
        _cartRepository = Substitute.For<ICartRepository>();
        _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<ListCartsProfile>()));
        _handler = new ListCartsHandler(_cartRepository, _mapper);
    }

    [Fact(DisplayName = "Given valid command When cart exists When get carts Then returns PaginatedResult Carts")]
    public async Task Handle_ValidCommand_Returns_PaginatedResult_Carts()
    {
        // Given
        var command = ListCartsHandlerTestData.GenerateValidCommand();
        var totalCount = new Random().Next(1, 20);
        var paginatedResultCarts = ListCartsHandlerTestData.GeneratePaginatedResultCarts(totalCount, command.Page, command.Size);

        // When
        _cartRepository.GetAllAsync(
            command.Page,
            command.Size,
            Arg.Any<string>(),
            cancellationToken: CancellationToken.None)
            .Returns(paginatedResultCarts);

        // When
        var listCartsResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        listCartsResult.Should().NotBeNull();
        listCartsResult.TotalCount.Should().Be(totalCount);
        listCartsResult.CurrentPage.Should().Be(command.Page);
        listCartsResult.PageSize.Should().Be(command.Size);
        listCartsResult.TotalPages.Should().Be((int)Math.Ceiling((double)totalCount / command.Size));

        await _cartRepository.Received(1).GetAllAsync(command.Page, command.Size, Arg.Any<string>(), CancellationToken.None);
    }

    [Fact(DisplayName = "Given a invalid command When get carts Then throws validation exception")]
    public async Task Handle_InvalidCommand_Returns_Validation_Exception()
    {
        // Given
        var command = ListCartsHandlerTestData.GenerateInvalidCommand();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    [Fact(DisplayName = "Given a valid command When carts not exists When get carts Then throws a NotFoundException")]
    public async Task Handle_ValidCommand_With_CartID_Not_Found_Returns_NotFoundException()
    {
        // Given
        var command = ListCartsHandlerTestData.GenerateValidCommand();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<NotFoundException>();
    }

}