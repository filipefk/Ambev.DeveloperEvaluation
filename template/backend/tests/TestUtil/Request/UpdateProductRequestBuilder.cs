using Ambev.DeveloperEvaluation.WebApi.Features.Product;
using Ambev.DeveloperEvaluation.WebApi.Features.Product.UpdateProduct;
using Bogus;

namespace TestUtil.Request;

public class UpdateProductRequestBuilder
{
    public static UpdateProductRequest Build()
    {
        return new Faker<UpdateProductRequest>()
            .RuleFor(p => p.Title, f => f.Commerce.ProductName())
            .RuleFor(p => p.Price, (f, p) => decimal.Parse(f.Commerce.Price()))
            .RuleFor(p => p.Description, f => f.Lorem.Paragraph())
            .RuleFor(p => p.Category, f => f.Commerce.Categories(1)[0])
            .RuleFor(p => p.Image, f => f.Image.PicsumUrl())
            .RuleFor(p => p.Rating, f => new BaseRatingApi
            {
                Rate = f.Random.Decimal(1, 5),
                Count = f.Random.Int(1, 1000)
            });
    }
}
