using Ambev.DeveloperEvaluation.Application.Product;
using Ambev.DeveloperEvaluation.Application.Product.ListProducts;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Product.ListProducts;

public class ListProductsProfile : Profile
{
    public ListProductsProfile()
    {
        CreateMap<ListProductsRequest, ListProductsCommand>();
        CreateMap<BaseProductResult, BaseProductResponse>();
        CreateMap<BaseRatingApp, BaseRatingApi>();
    }
}
