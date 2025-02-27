using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Product.ListProducts;

public class ListProductsProfile : Profile
{
    public ListProductsProfile()
    {
        CreateMap<Domain.Entities.Product, BaseProductResult>();
        CreateMap<Rating, BaseRatingApp>();
    }
}
