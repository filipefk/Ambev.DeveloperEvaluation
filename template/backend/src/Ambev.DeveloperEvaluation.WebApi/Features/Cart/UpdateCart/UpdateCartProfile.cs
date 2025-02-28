using Ambev.DeveloperEvaluation.Application.Cart;
using Ambev.DeveloperEvaluation.Application.Cart.UpdateCart;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Cart.UpdateCart;

public class UpdateCartProfile : Profile
{
    public UpdateCartProfile()
    {
        CreateMap<UpdateCartRequest, UpdateCartCommand>();
        CreateMap<BaseCartProductApi, BaseCartProductApp>();

        CreateMap<UpdateCartResult, UpdateCartResponse>()
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("dd/MM/yyyy")));

        CreateMap<BaseCartProductApp, BaseCartProductApi>();
    }
}
