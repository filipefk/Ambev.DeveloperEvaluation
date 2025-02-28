using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;
using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sale.TestData
{
    public static class SaleTestData
    {
        private static readonly Faker<ProductSold> productSoldFaker = new Faker<ProductSold>()
            .RuleFor(p => p.Id, f => Guid.NewGuid())
            .RuleFor(p => p.ProductId, f => Guid.NewGuid())
            .RuleFor(p => p.Title, f => f.Commerce.ProductName())
            .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
            .RuleFor(p => p.Quantity, f => f.Random.Int(1, 20))
            .RuleFor(p => p.Price, f => f.Finance.Amount(1, 100))
            .RuleFor(p => p.SoldPrice, (f, p) => p.Price)
            .RuleFor(p => p.Category, f => f.Commerce.Categories(1)[0])
            .RuleFor(p => p.Product, f => null!);

        private static readonly Faker<SaleDiscount> saleDiscountFaker = new Faker<SaleDiscount>()
            .RuleFor(d => d.Id, f => Guid.NewGuid())
            .RuleFor(d => d.DiscountPercentage, f => f.Random.Decimal(0, 100))
            .RuleFor(d => d.Reason, f => f.Lorem.Sentence());

        private static readonly Faker<DeveloperEvaluation.Domain.Entities.Sale> createSaleFaker = new Faker<DeveloperEvaluation.Domain.Entities.Sale>()
            .RuleFor(s => s.Id, f => Guid.NewGuid())
            .RuleFor(s => s.UserId, f => Guid.NewGuid())
            .RuleFor(s => s.BranchId, f => Guid.NewGuid())
            .RuleFor(s => s.SaleNumber, f => f.Random.Long(1, 100000))
            .RuleFor(s => s.Date, f => f.Date.Past())
            .RuleFor(s => s.UpdatedAt, f => f.Date.Recent())
            .RuleFor(s => s.Products, f => productSoldFaker.Generate(f.Random.Int(1, 5)))
            .RuleFor(s => s.Discounts, f => saleDiscountFaker.Generate(f.Random.Int(0, 3)))
            .RuleFor(s => s.SaleTotal, (f, s) => s.Products.Sum(p => p.SoldPrice * p.Quantity))
            .RuleFor(s => s.Canceled, f => f.Random.Bool())
            .RuleFor(s => s.User, f => null!)
            .RuleFor(s => s.Branch, f => null!);

        public static DeveloperEvaluation.Domain.Entities.Sale GenerateValidSale()
        {
            return createSaleFaker.Generate();
        }
    }
}
