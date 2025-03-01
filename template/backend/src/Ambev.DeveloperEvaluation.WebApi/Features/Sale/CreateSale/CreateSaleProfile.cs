using Ambev.DeveloperEvaluation.Application.Sale;
using Ambev.DeveloperEvaluation.Application.Sale.CreateSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.CreateSale;

public class CreateSaleProfile : Profile
{
    public CreateSaleProfile()
    {
        CreateMap<CreateSaleRequest, CreateSaleCommand>();

        CreateMap<CreateSaleResult, CreateSaleResponse>();
        CreateMap<BaseSaleProductApp, BaseSaleProductApi>();
        CreateMap<BaseSaleDiscountApp, BaseSaleDiscountApi>();
    }
}
