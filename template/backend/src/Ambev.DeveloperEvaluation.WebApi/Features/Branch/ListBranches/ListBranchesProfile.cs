using Ambev.DeveloperEvaluation.Application.Branch;
using Ambev.DeveloperEvaluation.Application.Branch.ListBranches;
using Ambev.DeveloperEvaluation.WebApi.Features.Cart.ListBranches;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branch.ListBranches;

public class ListBranchesProfile : Profile
{
    public ListBranchesProfile()
    {
        CreateMap<ListBranchesRequest, ListBranchesCommand>();
        CreateMap<BaseBranchResult, BaseBranchResponse>();
    }
}
