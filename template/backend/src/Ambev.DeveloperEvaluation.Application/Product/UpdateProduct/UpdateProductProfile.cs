using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Product.UpdateProduct;

public class UpdateProductProfile : Profile
{
    public UpdateProductProfile()
    {
        CreateMap<UpdateProductCommand, Domain.Entities.Product>();
        CreateMap<BaseRatingApp, Rating>();

        CreateMap<Domain.Entities.Product, UpdateProductResult>();
        CreateMap<Rating, BaseRatingApp>();
    }
}
