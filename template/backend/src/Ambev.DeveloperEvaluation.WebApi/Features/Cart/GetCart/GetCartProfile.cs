using Ambev.DeveloperEvaluation.Application.Cart;
using Ambev.DeveloperEvaluation.Application.Cart.GetCart;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Cart.GetCart;

public class GetCartProfile : Profile
{
    public GetCartProfile()
    {
        CreateMap<Guid, GetCartCommand>()
            .ConstructUsing(id => new GetCartCommand(id));

        CreateMap<GetCartResult, GetCartResponse>();
        CreateMap<BaseCartProductApp, BaseCartProductApi>();
    }
}
