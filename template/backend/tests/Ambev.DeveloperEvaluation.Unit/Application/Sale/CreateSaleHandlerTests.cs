using Ambev.DeveloperEvaluation.Application.Sale.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sale.Notification;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Sale.TestData;
using AutoMapper;
using FluentAssertions;
using MediatR;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sale;

/// <summary>
/// Contains unit tests for the <see cref="CreateSaleHandler"/> class.
/// </summary>
public class CreateSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly ICartRepository _cartRepository;
    private readonly IBranchRepository _branchRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly CreateSaleHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSaleHandlerTests"/> class.
    /// Sets up the test dependencies and creates fake data generators.
    /// </summary>
    public CreateSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _cartRepository = Substitute.For<ICartRepository>();
        _branchRepository = Substitute.For<IBranchRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<CreateSaleProfile>()));
        _mediator = Substitute.For<IMediator>();
        _handler = new CreateSaleHandler(_saleRepository, _cartRepository, _branchRepository, _unitOfWork, _mapper, _mediator);
    }

    /// <summary>
    /// Tests that a valid sale creation request is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid sale data When creating sale Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var command = CreateSaleHandlerTestData.GenerateValidCommand();
        var sale = SaleTestData.GenerateValidSale();
        sale.BranchId = command.BranchId;

        _branchRepository.GetByIdAsync(command.BranchId, Arg.Any<CancellationToken>()).Returns(new Branch { Id = Guid.NewGuid() });

        _cartRepository.GetByIdAsync(command.CartId, Arg.Any<CancellationToken>()).Returns(new DeveloperEvaluation.Domain.Entities.Cart { Id = Guid.NewGuid() });

        _saleRepository.CreateAsync(Arg.Any<DeveloperEvaluation.Domain.Entities.Sale>(), Arg.Any<CancellationToken>())
            .Returns(sale);

        // When
        var createSaleResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        createSaleResult.Should().NotBeNull();
        createSaleResult.Id.Should().Be(sale.Id);

        await _branchRepository.Received(1).GetByIdAsync(command.BranchId, Arg.Any<CancellationToken>());
        await _cartRepository.Received(1).GetByIdAsync(command.CartId, Arg.Any<CancellationToken>());
        await _saleRepository.Received(1).CreateAsync(Arg.Any<DeveloperEvaluation.Domain.Entities.Sale>(), Arg.Any<CancellationToken>());
        await _unitOfWork.Received(1).CommitAsync(Arg.Any<CancellationToken>());
        await _mediator.Received(1).Publish(Arg.Any<SaleCreatedNotification>(), Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that an invalid sale creation request throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid sale data When creating sale Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new CreateSaleCommand(); // Empty command will fail validation

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();

        await _branchRepository.Received(0).GetByIdAsync(command.BranchId, Arg.Any<CancellationToken>());
        await _cartRepository.Received(0).GetByIdAsync(command.CartId, Arg.Any<CancellationToken>());
        await _saleRepository.Received(0).CreateAsync(Arg.Any<DeveloperEvaluation.Domain.Entities.Sale>(), Arg.Any<CancellationToken>());
        await _unitOfWork.Received(0).CommitAsync(Arg.Any<CancellationToken>());
        await _mediator.Received(0).Publish(Arg.Any<SaleCreatedNotification>(), Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that a valid sale creation request with a invalid branch ID throws a validation NotFoundException.
    /// </summary>
    [Fact(DisplayName = "Given valid sale data with a invalid branch ID When creating sale Then throws NotFoundException")]
    public async Task Handle_ValidRequest_With_invalid_Branch_Throws_NotFoundException()
    {
        // Given
        var command = CreateSaleHandlerTestData.GenerateValidCommand();
        var sale = SaleTestData.GenerateValidSale();
        sale.BranchId = command.BranchId;

        _branchRepository.GetByIdAsync(command.BranchId, Arg.Any<CancellationToken>()).Returns((Branch)null!);

        _cartRepository.GetByIdAsync(command.CartId, Arg.Any<CancellationToken>()).Returns(new DeveloperEvaluation.Domain.Entities.Cart { Id = Guid.NewGuid() });

        _saleRepository.CreateAsync(Arg.Any<DeveloperEvaluation.Domain.Entities.Sale>(), Arg.Any<CancellationToken>())
            .Returns(sale);

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<NotFoundException>();

        await _branchRepository.Received(1).GetByIdAsync(command.BranchId, Arg.Any<CancellationToken>());
        await _cartRepository.Received(0).GetByIdAsync(command.CartId, Arg.Any<CancellationToken>());
        await _saleRepository.Received(0).CreateAsync(Arg.Any<DeveloperEvaluation.Domain.Entities.Sale>(), Arg.Any<CancellationToken>());
        await _unitOfWork.Received(0).CommitAsync(Arg.Any<CancellationToken>());
        await _mediator.Received(0).Publish(Arg.Any<SaleCreatedNotification>(), Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that a valid sale creation request with a invalid cart ID throws a validation NotFoundException.
    /// </summary>
    [Fact(DisplayName = "Given valid sale data with a invalid cart ID When creating sale Then throws NotFoundException")]
    public async Task Handle_ValidRequest_With_invalid_Cart_Throws_NotFoundException()
    {
        // Given
        var command = CreateSaleHandlerTestData.GenerateValidCommand();
        var sale = SaleTestData.GenerateValidSale();
        sale.BranchId = command.BranchId;

        _branchRepository.GetByIdAsync(command.BranchId, Arg.Any<CancellationToken>()).Returns(new Branch { Id = Guid.NewGuid() });

        _cartRepository.GetByIdAsync(command.CartId, Arg.Any<CancellationToken>()).Returns((DeveloperEvaluation.Domain.Entities.Cart)null!);

        _saleRepository.CreateAsync(Arg.Any<DeveloperEvaluation.Domain.Entities.Sale>(), Arg.Any<CancellationToken>())
            .Returns(sale);

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<NotFoundException>();

        await _branchRepository.Received(1).GetByIdAsync(command.BranchId, Arg.Any<CancellationToken>());
        await _cartRepository.Received(1).GetByIdAsync(command.CartId, Arg.Any<CancellationToken>());
        await _saleRepository.Received(0).CreateAsync(Arg.Any<DeveloperEvaluation.Domain.Entities.Sale>(), Arg.Any<CancellationToken>());
        await _unitOfWork.Received(0).CommitAsync(Arg.Any<CancellationToken>());
        await _mediator.Received(0).Publish(Arg.Any<SaleCreatedNotification>(), Arg.Any<CancellationToken>());
    }

}
