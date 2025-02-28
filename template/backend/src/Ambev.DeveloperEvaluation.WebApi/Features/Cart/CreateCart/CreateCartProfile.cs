using Ambev.DeveloperEvaluation.Application.Cart;
using Ambev.DeveloperEvaluation.Application.Cart.CreateCart;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Cart.CreateCart;

public class CreateCartProfile : Profile
{
    public CreateCartProfile()
    {
        CreateMap<CreateCartRequest, CreateCartCommand>();
        CreateMap<BaseCartProductApi, BaseCartProductApp>();

        CreateMap<CreateCartResult, CreateCartResponse>();
        CreateMap<BaseCartProductApp, BaseCartProductApi>();
    }
}
