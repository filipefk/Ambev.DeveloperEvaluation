using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Branch.ListBranches;

public class ListBranchesProfile : Profile
{
    public ListBranchesProfile()
    {
        CreateMap<Domain.Entities.Branch, BaseBranchResult>();
    }
}
