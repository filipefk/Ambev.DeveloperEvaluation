using Ambev.DeveloperEvaluation.Application.Branch.ListBranches;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Branch.ListBranches;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branch;

[Authorize]
public class BranchController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public BranchController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResponse<ListBranchesResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetBranches(
        CancellationToken cancellationToken,
        [FromQuery] int? _page = 1,
        [FromQuery] int? _size = 10,
        [FromQuery] string? _order = null)
    {
        var command = new ListBranchesCommand()
        {
            Page = _page ?? 1,
            Size = _size ?? 10,
            Order = _order
        };

        var result = await _mediator.Send(command, cancellationToken);
        var listResponse = _mapper.Map<List<BaseBranchResponse>>(result.Branches);

        var paginatedResponse = new PaginatedList<BaseBranchResponse>(
            listResponse,
            result.TotalCount,
            result.CurrentPage,
            result.PageSize);

        return OkPaginated(paginatedResponse);
    }
}
