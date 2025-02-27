using Ambev.DeveloperEvaluation.Application.Users.UpdateUser;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class UpdateUserHandlerTests
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;
    private readonly UpdateUserHandler _handler;

    public UpdateUserHandlerTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<UpdateUserProfile>()));
        _passwordHasher = Substitute.For<IPasswordHasher>();
        _handler = new UpdateUserHandler(_userRepository, _unitOfWork, _mapper, _passwordHasher);
    }

    [Fact(DisplayName = "Given valid user data When updating user Then returns success response")]
    public async Task Handle_Valid_Command_Returns_Success_Response()
    {
        // Given
        var command = UpdateUserHandlerTestData.GenerateValidCommand();
        var user = UserTestData.GenerateValidUser();
        user.Id = command.Id;

        _userRepository.GetByEmailAsync(
            command.Email,
            Arg.Any<CancellationToken>())
            .Returns((User)null!);

        _userRepository.GetByIdAsync(
            command.Id,
            Arg.Any<CancellationToken>())
            .Returns(user);

        _passwordHasher.HashPassword(Arg.Any<string>()).Returns("hashedPassword");

        // When
        var updateUserResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        updateUserResult.Should().NotBeNull();
        updateUserResult.Id.Should().Be(command.Id);
        updateUserResult.UserName.Should().Be(command.Username);
        updateUserResult.Email.Should().Be(command.Email);
        updateUserResult.Phone.Should().Be(command.Phone);
        updateUserResult.Status.Should().Be(command.Status.ToString());
        updateUserResult.Role.Should().Be(command.Role.ToString());

        await _userRepository.Received(1).GetByEmailAsync(command.Email, CancellationToken.None);
        await _userRepository.Received(1).GetByIdAsync(command.Id, CancellationToken.None);
        await _unitOfWork.Received(1).CommitAsync(Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given invalid user data When updating user Then throws validation exception")]
    public async Task Handle_Invalid_Command_Throws_ValidationException()
    {
        // Given
        var command = new UpdateUserCommand(); // Empty command will fail validation

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();

        await _userRepository.Received(0).GetByEmailAsync(Arg.Any<string>(), CancellationToken.None);
        await _userRepository.Received(0).GetByIdAsync(Arg.Any<Guid>(), CancellationToken.None);
        await _unitOfWork.Received(0).CommitAsync(Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given valid user data with existing email When updating user Then throws InvalidOperationException")]
    public async Task Handle_Valid_Command_With_Existing_Email_Throws_InvalidOperationException()
    {
        // Given
        var command = UpdateUserHandlerTestData.GenerateValidCommand();
        var user = UserTestData.GenerateValidUser();
        user.Id = command.Id;

        var otheUser = UserTestData.GenerateValidUser();
        otheUser.Email = command.Email;

        _userRepository.GetByEmailAsync(
            command.Email,
            Arg.Any<CancellationToken>())
            .Returns(otheUser);

        _userRepository.GetByIdAsync(
            command.Id,
            Arg.Any<CancellationToken>())
            .Returns(user);

        _passwordHasher.HashPassword(Arg.Any<string>()).Returns("hashedPassword");

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<InvalidOperationException>();

        await _userRepository.Received(1).GetByEmailAsync(command.Email, CancellationToken.None);
        await _userRepository.Received(0).GetByIdAsync(Arg.Any<Guid>(), CancellationToken.None);
        await _unitOfWork.Received(0).CommitAsync(Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given valid user data When updating user When user not exists Then throws InvalidOperationException")]
    public async Task Handle_Valid_Command_User_Not_Exists_Throws_InvalidOperationException()
    {
        // Given
        var command = UpdateUserHandlerTestData.GenerateValidCommand();

        _userRepository.GetByEmailAsync(
            command.Email,
            Arg.Any<CancellationToken>())
            .Returns((User)null!);

        _userRepository.GetByIdAsync(
            command.Id,
            Arg.Any<CancellationToken>())
            .Returns((User)null!);

        _passwordHasher.HashPassword(Arg.Any<string>()).Returns("hashedPassword");

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<InvalidOperationException>();

        await _userRepository.Received(1).GetByEmailAsync(command.Email, CancellationToken.None);
        await _userRepository.Received(1).GetByIdAsync(command.Id, CancellationToken.None);
        await _unitOfWork.Received(0).CommitAsync(Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that the password is hashed before saving the user.
    /// </summary>
    [Fact(DisplayName = "Given user updating command When handling Then password is hashed")]
    public async Task Handle_Valid_Command_Hashes_Password()
    {
        // Given
        var command = UpdateUserHandlerTestData.GenerateValidCommand();
        var originalPassword = command.Password;
        const string hashedPassword = "h@shedPassw0rd";
        var user = UserTestData.GenerateValidUser();
        user.Id = command.Id;

        _userRepository.GetByEmailAsync(
            command.Email,
            Arg.Any<CancellationToken>())
            .Returns((User)null!);

        _userRepository.GetByIdAsync(
            command.Id,
            Arg.Any<CancellationToken>())
            .Returns(user);

        _passwordHasher.HashPassword(originalPassword).Returns(hashedPassword);

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        _passwordHasher.Received(1).HashPassword(originalPassword);

        await _userRepository.Received(1).GetByEmailAsync(command.Email, CancellationToken.None);
        await _userRepository.Received(1).GetByIdAsync(command.Id, CancellationToken.None);
        await _unitOfWork.Received(1).CommitAsync(Arg.Any<CancellationToken>());

    }

}
