using Ambev.DeveloperEvaluation.Application.Sale;
using Ambev.DeveloperEvaluation.Application.Sale.UpdateSale;
using Bogus;
using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sale.TestData
{
    public static class UpdateSaleHandlerTestData
    {
        private static readonly Faker<BaseSaleProductApp> baseSaleProductAppFaker = new Faker<BaseSaleProductApp>()
            .RuleFor(p => p.ProductId, f => Guid.NewGuid())
            .RuleFor(p => p.Title, f => f.Commerce.ProductName())
            .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
            .RuleFor(p => p.Quantity, f => f.Random.Int(1, 20))
            .RuleFor(p => p.Price, f => f.Finance.Amount(1, 100))
            .RuleFor(p => p.SoldPrice, (f, p) => p.Price)
            .RuleFor(p => p.Category, f => f.Commerce.Categories(1)[0]);

        private static readonly Faker<UpdateSaleCommand> updateSaleHandlerFaker = new Faker<UpdateSaleCommand>()
            .RuleFor(c => c.Id, f => Guid.NewGuid())
            .RuleFor(c => c.UserId, f => Guid.NewGuid())
            .RuleFor(c => c.BranchId, f => Guid.NewGuid())
            .RuleFor(c => c.Date, f => f.Date.Past())
            .RuleFor(c => c.Products, f => baseSaleProductAppFaker.Generate(f.Random.Int(1, 5)))
            .RuleFor(c => c.Canceled, f => f.Random.Bool());

        public static UpdateSaleCommand GenerateValidCommand()
        {
            return updateSaleHandlerFaker.Generate();
        }
    }
}
