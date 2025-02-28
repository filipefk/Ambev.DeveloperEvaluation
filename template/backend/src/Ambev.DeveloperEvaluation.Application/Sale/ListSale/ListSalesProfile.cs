using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sale.ListSales;

public class ListSalesProfile : Profile
{
    public ListSalesProfile()
    {
        CreateMap<Domain.Entities.Sale, BaseSaleResult>();
        CreateMap<ProductSold, BaseSaleProductApp>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Product.Title))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Product.Description));
        CreateMap<SaleDiscount, BaseSaleDiscountApp>();
    }
}
