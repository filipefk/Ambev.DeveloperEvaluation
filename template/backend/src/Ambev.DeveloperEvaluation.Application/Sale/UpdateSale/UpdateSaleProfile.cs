using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sale.UpdateSale;

public class UpdateSaleProfile : Profile
{
    public UpdateSaleProfile()
    {
        CreateMap<UpdateSaleCommand, Domain.Entities.Sale>();
        CreateMap<BaseSaleProductApp, ProductSold>();
        //CreateMap<BaseSaleDiscountApp, SaleDiscount>();

        CreateMap<Domain.Entities.Sale, UpdateSaleResult>();
        CreateMap<ProductSold, BaseSaleProductApp>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Product.Title))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Product.Description));
        CreateMap<SaleDiscount, BaseSaleDiscountApp>();
    }
}
