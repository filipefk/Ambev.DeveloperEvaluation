using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Product.CreateProduct;

public class CreateProductProfile : Profile
{
    public CreateProductProfile()
    {
        CreateMap<CreateProductCommand, Domain.Entities.Product>();
        CreateMap<BaseRatingApp, Rating>();

        CreateMap<Domain.Entities.Product, CreateProductResult>();
        CreateMap<Rating, BaseRatingApp>();
    }
}
