using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Cart.UpdateCart;

public class UpdateCartProfile : Profile
{
    public UpdateCartProfile()
    {
        CreateMap<UpdateCartCommand, Domain.Entities.Cart>();
        CreateMap<BaseCartProductApp, CartProduct>();

        CreateMap<Domain.Entities.Cart, UpdateCartResult>();
        CreateMap<CartProduct, BaseCartProductApp>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Product.Title))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Product.Description));
    }
}
