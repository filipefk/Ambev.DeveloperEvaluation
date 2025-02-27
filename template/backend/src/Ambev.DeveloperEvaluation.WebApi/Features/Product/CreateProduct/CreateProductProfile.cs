using Ambev.DeveloperEvaluation.Application.Product;
using Ambev.DeveloperEvaluation.Application.Product.CreateProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Product.CreateProduct;

public class CreateProductProfile : Profile
{
    public CreateProductProfile()
    {
        CreateMap<CreateProductRequest, CreateProductCommand>();
        CreateMap<BaseRatingApi, BaseRatingApp>();

        CreateMap<CreateProductResult, CreateProductResponse>();
        CreateMap<BaseRatingApp, BaseRatingApi>();
    }
}
