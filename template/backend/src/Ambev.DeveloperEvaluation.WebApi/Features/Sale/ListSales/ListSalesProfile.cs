using Ambev.DeveloperEvaluation.Application.Sale;
using Ambev.DeveloperEvaluation.Application.Sale.ListSales;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.ListSales;

public class ListSalesProfile : Profile
{
    public ListSalesProfile()
    {
        CreateMap<ListSalesRequest, ListSalesCommand>();
        CreateMap<BaseSaleResult, BaseSaleResponse>()
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("dd/MM/yyyy")));

        CreateMap<BaseSaleProductApp, BaseSaleProductApi>();
        CreateMap<BaseSaleDiscountApp, BaseSaleDiscountApi>();
    }
}
