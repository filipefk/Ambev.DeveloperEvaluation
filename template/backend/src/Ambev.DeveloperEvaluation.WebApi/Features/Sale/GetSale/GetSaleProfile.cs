using Ambev.DeveloperEvaluation.Application.Sale;
using Ambev.DeveloperEvaluation.Application.Sale.GetSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.GetSale;

public class GetSaleProfile : Profile
{
    public GetSaleProfile()
    {
        CreateMap<Guid, GetSaleCommand>()
            .ConstructUsing(id => new GetSaleCommand(id));

        CreateMap<GetSaleResult, GetSaleResponse>();
        CreateMap<BaseSaleProductApp, BaseSaleProductApi>();
        CreateMap<BaseSaleDiscountApp, BaseSaleDiscountApi>();
    }
}
