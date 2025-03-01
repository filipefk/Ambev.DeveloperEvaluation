using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sale.CreateSale;

public class CreateSaleProfile : Profile
{
    public CreateSaleProfile()
    {
        CreateMap<BaseSaleProductApp, ProductSold>();

        CreateMap<Domain.Entities.Sale, CreateSaleResult>();
        CreateMap<ProductSold, BaseSaleProductApp>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Product.Title))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Product.Description));

        CreateMap<SaleDiscount, BaseSaleDiscountApp>();
    }
}
