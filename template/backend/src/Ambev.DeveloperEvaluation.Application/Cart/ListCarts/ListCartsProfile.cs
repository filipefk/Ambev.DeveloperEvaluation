using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Cart.ListCarts;

public class ListCartsProfile : Profile
{
    public ListCartsProfile()
    {
        CreateMap<Domain.Entities.Cart, BaseCartResult>();
        CreateMap<CartProduct, BaseCartProductApp>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Product.Title))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Product.Description));
    }
}
