using Ambev.DeveloperEvaluation.Application.Cart;
using Ambev.DeveloperEvaluation.Application.Cart.ListCarts;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Cart.ListCarts;

public class ListCartsProfile : Profile
{
    public ListCartsProfile()
    {
        CreateMap<ListCartsRequest, ListCartsCommand>();
        CreateMap<BaseCartResult, BaseCartResponse>()
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("dd/MM/yyyy")));

        CreateMap<BaseCartProductApp, BaseCartProductApi>();
    }
}
