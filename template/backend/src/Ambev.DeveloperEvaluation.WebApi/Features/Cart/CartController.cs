using Ambev.DeveloperEvaluation.Application.Cart.CreateCart;
using Ambev.DeveloperEvaluation.Application.Cart.DeleteCart;
using Ambev.DeveloperEvaluation.Application.Cart.GetCart;
using Ambev.DeveloperEvaluation.Application.Cart.ListCarts;
using Ambev.DeveloperEvaluation.Application.Cart.UpdateCart;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Cart.CreateCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Cart.DeleteCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Cart.GetCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Cart.ListCarts;
using Ambev.DeveloperEvaluation.WebApi.Features.Cart.UpdateCart;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Cart;

[Authorize]
public class CartController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CartController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateCartResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateCart(
        [FromBody] CreateCartRequest request,
        CancellationToken cancellationToken)
    {
        var validator = new CreateCartRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<CreateCartCommand>(request);
        var result = await _mediator.Send(command, cancellationToken);
        var response = _mapper.Map<CreateCartResponse>(result);

        return Created(string.Empty, null!, response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetCartResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetCart(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var request = new GetCartRequest { Id = id };
        var validator = new GetCartRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<GetCartCommand>(request.Id);
        var result = await _mediator.Send(command, cancellationToken);
        var response = _mapper.Map<GetCartResponse>(result);

        return Ok(response);

    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DeleteCart(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var request = new DeleteCartRequest { Id = id };
        var validator = new DeleteCartRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<DeleteCartCommand>(request.Id);
        await _mediator.Send(command, cancellationToken);

        return OkSimple("Cart deleted successfully");
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<UpdateCartResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UpdateCart(
        [FromRoute] Guid id,
        [FromBody] UpdateCartRequest request,
        CancellationToken cancellationToken)
    {

        var validator = new UpdateCartRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<UpdateCartCommand>(request);
        command.Id = id;

        var result = await _mediator.Send(command, cancellationToken);
        var response = _mapper.Map<UpdateCartResponse>(result);

        return Ok(response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResponse<ListCartsResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetCarts(
        CancellationToken cancellationToken,
        [FromQuery] int? _page = 1,
        [FromQuery] int? _size = 10,
        [FromQuery] string? _order = null)
    {
        var command = new ListCartsCommand()
        {
            Page = _page ?? 1,
            Size = _size ?? 10,
            Order = _order
        };

        var result = await _mediator.Send(command, cancellationToken);
        var listResponse = _mapper.Map<List<BaseCartResponse>>(result.Carts);

        var paginatedResponse = new PaginatedList<BaseCartResponse>(
            listResponse,
            result.TotalCount,
            result.CurrentPage,
            result.PageSize);

        return OkPaginated(paginatedResponse);
    }
}
