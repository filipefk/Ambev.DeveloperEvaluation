using Ambev.DeveloperEvaluation.Application.Product;
using Ambev.DeveloperEvaluation.Application.Product.UpdateProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Product.UpdateProduct;

public class UpdateProductProfile : Profile
{
    public UpdateProductProfile()
    {
        CreateMap<UpdateProductRequest, UpdateProductCommand>();
        CreateMap<BaseRatingApi, BaseRatingApp>();

        CreateMap<UpdateProductResult, UpdateProductResponse>();
        CreateMap<BaseRatingApp, BaseRatingApi>();
    }
}
