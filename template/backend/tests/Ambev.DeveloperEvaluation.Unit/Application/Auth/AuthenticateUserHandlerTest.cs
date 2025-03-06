using Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using Moq;
using NSubstitute;
using TestUtil.Command;
using TestUtil.Entities;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Auth;

public class AuthenticateUserHandlerTest
{
    private readonly Mock<IUserRepository> _userRepository;
    private readonly Mock<IPasswordHasher> _passwordHasher;
    private readonly Mock<IJwtTokenGenerator> _jwtTokenGenerator;
    private readonly IMapper _mapper;
    private readonly AuthenticateUserHandler _handler;

    public AuthenticateUserHandlerTest()
    {
        _userRepository = new Mock<IUserRepository>();
        _passwordHasher = new Mock<IPasswordHasher>();
        _jwtTokenGenerator = new Mock<IJwtTokenGenerator>();
        _mapper = new MapperConfiguration(cfg => cfg.AddProfile<AuthenticateUserProfile>()).CreateMapper();
        _handler = new AuthenticateUserHandler(_userRepository.Object, _passwordHasher.Object, _jwtTokenGenerator.Object, _mapper);
    }

    [Fact(DisplayName = "Given valid user and password When authenticate Then returns success response")]
    public async Task Handle_ValidRequest_Returns_Success_Response()
    {
        // Given
        var user = UserTestData.GenerateValidUser();
        user.Status = DeveloperEvaluation.Domain.Enums.UserStatus.Active;

        var command = AuthenticateUserCommandBuilder.Build(user);

        _userRepository.Setup(repository => repository.GetByEmailAsync(command.Email, It.IsAny<CancellationToken>())).ReturnsAsync(user);

        _passwordHasher.Setup(hasher => hasher.VerifyPassword(command.Password, user.Password)).Returns(true);

        _jwtTokenGenerator.Setup(generator => generator.GenerateToken(user)).Returns("token");

        // When
        var authenticateUserResult = await _handler.Handle(command, Arg.Any<CancellationToken>());

        // Then
        authenticateUserResult.Should().NotBeNull();
        authenticateUserResult.Token.Should().Be("token");
        authenticateUserResult.Email.Should().Be(user.Email);
        authenticateUserResult.UserName.Should().Be(user.Username);
        authenticateUserResult.Role.Should().Be(user.Role.ToString());

        _userRepository.Verify(repository => repository.GetByEmailAsync(command.Email, It.IsAny<CancellationToken>()), Times.Once);
        _passwordHasher.Verify(hasher => hasher.VerifyPassword(command.Password, user.Password), Times.Once);
        _jwtTokenGenerator.Verify(generator => generator.GenerateToken(user), Times.Once);
    }

    [Fact(DisplayName = "Given invalid user and password When authenticate Then throws UnauthorizedException")]
    public async Task Handle_Invalid_User_And_Password_Throws_UnauthorizedException()
    {
        // Given
        var user = UserTestData.GenerateValidUser();
        user.Status = DeveloperEvaluation.Domain.Enums.UserStatus.Active;

        var command = AuthenticateUserCommandBuilder.Build(user);

        _passwordHasher.Setup(hasher => hasher.VerifyPassword(command.Password, user.Password)).Returns(true);

        _jwtTokenGenerator.Setup(generator => generator.GenerateToken(user)).Returns("token");

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<UnauthorizedException>();

        _userRepository.Verify(repository => repository.GetByEmailAsync(command.Email, It.IsAny<CancellationToken>()), Times.Once);
        _passwordHasher.Verify(hasher => hasher.VerifyPassword(command.Password, user.Password), Times.Never);
        _jwtTokenGenerator.Verify(generator => generator.GenerateToken(user), Times.Never);
    }

    [Fact(DisplayName = "Given invalid password When authenticate Then throws UnauthorizedException")]
    public async Task Handle_Invalid_Password_Throws_UnauthorizedException()
    {
        // Given
        var user = UserTestData.GenerateValidUser();
        user.Status = DeveloperEvaluation.Domain.Enums.UserStatus.Active;

        var command = AuthenticateUserCommandBuilder.Build(user);

        _userRepository.Setup(repository => repository.GetByEmailAsync(command.Email, It.IsAny<CancellationToken>())).ReturnsAsync(user);

        _passwordHasher.Setup(hasher => hasher.VerifyPassword(command.Password, user.Password)).Returns(false);

        _jwtTokenGenerator.Setup(generator => generator.GenerateToken(user)).Returns("token");

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<UnauthorizedException>();

        _userRepository.Verify(repository => repository.GetByEmailAsync(command.Email, It.IsAny<CancellationToken>()), Times.Once);
        _passwordHasher.Verify(hasher => hasher.VerifyPassword(command.Password, user.Password), Times.Once);
        _jwtTokenGenerator.Verify(generator => generator.GenerateToken(user), Times.Never);
    }

    [Fact(DisplayName = "Given inactive user When authenticate Then throws UnauthorizedException")]
    public async Task Handle_Inactive_User_Throws_UnauthorizedException()
    {
        // Given
        var user = UserTestData.GenerateValidUser();
        user.Status = DeveloperEvaluation.Domain.Enums.UserStatus.Inactive;

        var command = AuthenticateUserCommandBuilder.Build(user);

        _userRepository.Setup(repository => repository.GetByEmailAsync(command.Email, It.IsAny<CancellationToken>())).ReturnsAsync(user);

        _passwordHasher.Setup(hasher => hasher.VerifyPassword(command.Password, user.Password)).Returns(true);

        _jwtTokenGenerator.Setup(generator => generator.GenerateToken(user)).Returns("token");

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<UnauthorizedException>();

        _userRepository.Verify(repository => repository.GetByEmailAsync(command.Email, It.IsAny<CancellationToken>()), Times.Once);
        _passwordHasher.Verify(hasher => hasher.VerifyPassword(command.Password, user.Password), Times.Once);
        _jwtTokenGenerator.Verify(generator => generator.GenerateToken(user), Times.Never);
    }
}
