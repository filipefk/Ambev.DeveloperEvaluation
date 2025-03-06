using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Users.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using TestUtil.Entities;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users;

public class GetUserHandlerTests
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly GetUserHandler _handler;

    public GetUserHandlerTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<GetUserProfile>()));
        _handler = new GetUserHandler(_userRepository, _mapper);
    }

    [Fact(DisplayName = "Given valid command When user exists When get user Then returns user")]
    public async Task Handle_ValidCommand_Returns_User()
    {
        // Given
        var command = GetUserHandlerTestData.GenerateValidCommand();
        var user = UserTestData.GenerateValidUser();
        user.Id = command.Id;

        // When
        _userRepository.GetByIdAsync(
            command.Id,
            Arg.Any<CancellationToken>())
            .Returns(user);

        // When
        var getUserResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        getUserResult.Should().NotBeNull();
        getUserResult.Id.Should().Be(user.Id);
        getUserResult.UserName.Should().Be(user.Username);
        getUserResult.Email.Should().Be(user.Email);
        getUserResult.Phone.Should().Be(user.Phone);
        getUserResult.Status.Should().Be(user.Status.ToString());
        getUserResult.Role.Should().Be(user.Role.ToString());

        await _userRepository.Received(1).GetByIdAsync(command.Id, CancellationToken.None);
    }

    [Fact(DisplayName = "Given a invalid command When get user Then throws validation exception")]
    public async Task Handle_InvalidCommand_Returns_Validation_Exception()
    {
        // Given
        var command = GetUserHandlerTestData.GenerateInvalidCommand();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    [Fact(DisplayName = "Given a valid command When user not exists When get user Then throws a NotFoundException")]
    public async Task Handle_ValidCommand_With_UserID_Not_Found_Returns_NotFoundException()
    {
        // Given
        var command = GetUserHandlerTestData.GenerateValidCommand();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<NotFoundException>();
    }
}