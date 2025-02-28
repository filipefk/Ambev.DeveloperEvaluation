using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Cart.CreateCart;

public class CreateCartProfile : Profile
{
    public CreateCartProfile()
    {
        CreateMap<CreateCartCommand, Domain.Entities.Cart>();
        CreateMap<BaseCartProductApp, CartProduct>();

        CreateMap<Domain.Entities.Cart, CreateCartResult>();
        CreateMap<CartProduct, BaseCartProductApp>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Product.Title))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Product.Description));
    }
}
