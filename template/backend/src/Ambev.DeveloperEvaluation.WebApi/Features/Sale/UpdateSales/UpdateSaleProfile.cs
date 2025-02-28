using Ambev.DeveloperEvaluation.Application.Sale;
using Ambev.DeveloperEvaluation.Application.Sale.UpdateSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.UpdateSale;

public class UpdateSaleProfile : Profile
{
    public UpdateSaleProfile()
    {
        CreateMap<UpdateSaleRequest, UpdateSaleCommand>();
        CreateMap<BaseSaleProductApi, BaseSaleProductApp>();
        CreateMap<BaseSaleDiscountApi, BaseSaleDiscountApp>();

        CreateMap<UpdateSaleResult, UpdateSaleResponse>()
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("dd/MM/yyyy")));

        CreateMap<BaseSaleProductApp, BaseSaleProductApi>();
        CreateMap<BaseSaleDiscountApp, BaseSaleDiscountApi>();
    }
}
