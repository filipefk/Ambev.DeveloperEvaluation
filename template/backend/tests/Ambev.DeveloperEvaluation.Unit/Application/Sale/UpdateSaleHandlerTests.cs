using Ambev.DeveloperEvaluation.Application.Sale.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Sale.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sale;

public class UpdateSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IUserRepository _userRepository;
    private readonly IBranchRepository _branchRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly UpdateSaleHandler _handler;

    public UpdateSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _userRepository = Substitute.For<IUserRepository>();
        _branchRepository = Substitute.For<IBranchRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<UpdateSaleProfile>()));
        _handler = new UpdateSaleHandler(_saleRepository, _branchRepository, _userRepository, _unitOfWork, _mapper);
    }

    [Fact(DisplayName = "Given valid sale data When updating sale Then returns success response")]
    public async Task Handle_Valid_Command_Returns_Success_Response()
    {
        // Given
        var command = UpdateSaleHandlerTestData.GenerateValidCommand();
        var sale = SaleTestData.GenerateValidSale();
        sale.Id = command.Id;

        _userRepository.GetByIdAsync(
            command.UserId,
            Arg.Any<CancellationToken>())
            .Returns(new DeveloperEvaluation.Domain.Entities.User());

        _branchRepository.GetByIdAsync(command.BranchId, Arg.Any<CancellationToken>())
            .Returns(new DeveloperEvaluation.Domain.Entities.Branch());

        _saleRepository.GetByIdAsync(
            command.Id,
            Arg.Any<CancellationToken>())
            .Returns(sale);

        // When
        var updateSaleResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        updateSaleResult.Should().NotBeNull();
        updateSaleResult.Id.Should().Be(command.Id);
        updateSaleResult.UserId.Should().Be(sale.UserId);

        await _userRepository.Received(1).GetByIdAsync(command.UserId, CancellationToken.None);
        await _branchRepository.Received(1).GetByIdAsync(command.BranchId, CancellationToken.None);
        await _saleRepository.Received(1).GetByIdAsync(command.Id, CancellationToken.None);
        await _unitOfWork.Received(1).CommitAsync(Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given invalid sale data When updating sale Then throws validation exception")]
    public async Task Handle_Invalid_Command_Throws_ValidationException()
    {
        // Given
        var command = new UpdateSaleCommand() // Empty command will fail validation
        {
            Id = Guid.NewGuid()
        };
        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();

        await _userRepository.Received(0).GetByIdAsync(command.UserId, CancellationToken.None);
        await _branchRepository.Received(0).GetByIdAsync(command.BranchId, CancellationToken.None);
        await _saleRepository.Received(0).GetByIdAsync(Arg.Any<Guid>(), CancellationToken.None);
        await _unitOfWork.Received(0).CommitAsync(Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given valid sale data When updating sale When sale not exists Then throws NotFoundException")]
    public async Task Handle_Valid_Command_Sale_Not_Exists_Throws_NotFoundException()
    {
        // Given
        var command = UpdateSaleHandlerTestData.GenerateValidCommand();

        _userRepository.GetByIdAsync(
            command.UserId,
            Arg.Any<CancellationToken>())
            .Returns(new DeveloperEvaluation.Domain.Entities.User());

        _branchRepository.GetByIdAsync(command.BranchId, Arg.Any<CancellationToken>())
            .Returns(new DeveloperEvaluation.Domain.Entities.Branch());

        _saleRepository.GetByIdAsync(
            command.Id,
            Arg.Any<CancellationToken>())
            .Returns((DeveloperEvaluation.Domain.Entities.Sale)null!);

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<NotFoundException>();

        await _saleRepository.Received(1).GetByIdAsync(command.Id, CancellationToken.None);
        await _branchRepository.Received(0).GetByIdAsync(command.BranchId, CancellationToken.None);
        await _userRepository.Received(0).GetByIdAsync(command.UserId, CancellationToken.None);
        await _unitOfWork.Received(0).CommitAsync(Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given valid sale data When updating sale When branch not exists Then throws NotFoundException")]
    public async Task Handle_Valid_Command_Branch_Not_Exists_Throws_NotFoundException()
    {
        // Given
        var command = UpdateSaleHandlerTestData.GenerateValidCommand();
        var sale = SaleTestData.GenerateValidSale();

        _userRepository.GetByIdAsync(
            command.UserId,
            Arg.Any<CancellationToken>())
            .Returns(new User());

        _branchRepository.GetByIdAsync(command.BranchId, Arg.Any<CancellationToken>())
            .Returns((Branch)null!);

        _saleRepository.GetByIdAsync(
            command.Id,
            Arg.Any<CancellationToken>())
            .Returns(sale);

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<NotFoundException>();

        await _saleRepository.Received(1).GetByIdAsync(command.Id, CancellationToken.None);
        await _branchRepository.Received(1).GetByIdAsync(command.BranchId, CancellationToken.None);
        await _userRepository.Received(0).GetByIdAsync(command.UserId, CancellationToken.None);
        await _unitOfWork.Received(0).CommitAsync(Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given valid sale data When updating sale When user not exists Then throws NotFoundException")]
    public async Task Handle_Valid_Command_User_Not_Exists_Throws_NotFoundException()
    {
        // Given
        var command = UpdateSaleHandlerTestData.GenerateValidCommand();
        var sale = SaleTestData.GenerateValidSale();

        _userRepository.GetByIdAsync(
            command.UserId,
            Arg.Any<CancellationToken>())
            .Returns((User)null!);

        _branchRepository.GetByIdAsync(command.BranchId, Arg.Any<CancellationToken>())
            .Returns(new Branch());

        _saleRepository.GetByIdAsync(
            command.Id,
            Arg.Any<CancellationToken>())
            .Returns(sale);

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<NotFoundException>();
        
        await _saleRepository.Received(1).GetByIdAsync(command.Id, CancellationToken.None);
        await _branchRepository.Received(1).GetByIdAsync(command.BranchId, CancellationToken.None);
        await _userRepository.Received(1).GetByIdAsync(command.UserId, CancellationToken.None);
        await _unitOfWork.Received(0).CommitAsync(Arg.Any<CancellationToken>());
    }
}
