using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Product.GetProduct;

public class GetProductProfile : Profile
{
    public GetProductProfile()
    {
        CreateMap<Domain.Entities.Product, GetProductResult>();
        CreateMap<Rating, BaseRatingApp>();
    }
}
