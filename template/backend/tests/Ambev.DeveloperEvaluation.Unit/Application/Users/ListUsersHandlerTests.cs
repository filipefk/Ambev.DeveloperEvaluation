using Ambev.DeveloperEvaluation.Application.Users.ListUsers;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Users.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users;

public class ListUsersHandlerTests
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ListUsersHandler _handler;

    public ListUsersHandlerTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<ListUsersProfile>()));
        _handler = new ListUsersHandler(_userRepository, _mapper);
    }

    [Fact(DisplayName = "Given valid command When user exists When get users Then returns PaginatedResult Users")]
    public async Task Handle_ValidCommand_Returns_PaginatedResult_Users()
    {
        // Given
        var command = ListUsersHandlerTestData.GenerateValidCommand();
        var totalCount = new Random().Next(1, 20);
        var paginatedResultUsers = ListUsersHandlerTestData.GeneratePaginatedResultUsers(totalCount, command.Page, command.Size);

        // When
        _userRepository.GetAllAsync(
            command.Page,
            command.Size,
            Arg.Any<string>(),
            cancellationToken: CancellationToken.None)
            .Returns(paginatedResultUsers);

        // When
        var listUsersResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        listUsersResult.Should().NotBeNull();
        listUsersResult.TotalCount.Should().Be(totalCount);
        listUsersResult.CurrentPage.Should().Be(command.Page);
        listUsersResult.PageSize.Should().Be(command.Size);
        listUsersResult.TotalPages.Should().Be((int)Math.Ceiling((double)totalCount / command.Size));
        listUsersResult.Users.Count.Should().Be(paginatedResultUsers.Results.Count);

        await _userRepository.Received(1).GetAllAsync(command.Page, command.Size, Arg.Any<string>(), CancellationToken.None);
    }

    [Fact(DisplayName = "Given a invalid command When get users Then throws validation exception")]
    public async Task Handle_InvalidCommand_Returns_Validation_Exception()
    {
        // Given
        var command = ListUsersHandlerTestData.GenerateInvalidCommand();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    [Fact(DisplayName = "Given a valid command When users not exists When get users Then throws a InvalidOperationException")]
    public async Task Handle_ValidCommand_With_UserID_Not_Found_Returns_KeyNotFoundException_Exception()
    {
        // Given
        var command = ListUsersHandlerTestData.GenerateValidCommand();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<InvalidOperationException>();
    }

}