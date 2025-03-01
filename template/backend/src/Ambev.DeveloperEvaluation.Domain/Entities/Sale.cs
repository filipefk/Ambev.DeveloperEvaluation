using Ambev.DeveloperEvaluation.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Sale : BaseEntity
{
    public Guid UserId { get; set; }

    public Guid BranchId { get; set; }

    public long SaleNumber { get; set; }

    public DateTime Date { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public ICollection<ProductSold> Products { get; set; } = [];

    public ICollection<SaleDiscount> Discounts { get; set; } = [];

    public decimal SaleTotal { get; set; }

    public bool Canceled { get; set; } = false;

    public User User { get; set; } = null!;

    public Branch Branch { get; set; } = null!;

    public static Sale SaleFactory(Cart cart, Branch branch)
    {
        if (cart == null) throw new ArgumentNullException(nameof(cart));

        var sale = new Sale
        {
            UserId = cart.UserId,
            BranchId = branch.Id,
            Date = DateTime.UtcNow,
            Products = cart.Products.Select(p => new ProductSold
            {
                ProductId = p.ProductId,
                Title = p.Product.Title,
                Description = p.Product.Description,
                Quantity = p.Quantity,
                Price = p.Product.Price,
                SoldPrice = p.Product.Price,
                Category = p.Product.Category,
                Product = null!,
                Sale = null!
            }).ToList(),
            SaleTotal = cart.Products.Sum(p => p.Product.Price * p.Quantity),
            User = null!,
            Branch = null!,
        };

        sale.SetTotalPrice();

        return sale;
    }

    public void SetTotalPrice()
    {
        SaleTotal = Products.Sum(p => p.SoldPrice * p.Quantity);
    }

    public void ApplyDiscounts()
    {
        Discounts.Clear();

        foreach (var product in Products)
        {
            if (product.Quantity >= 4 && product.Quantity < 10)
            {
                Discounts.Add(new SaleDiscount
                {
                    SaleId = this.Id,
                    ProductSoldId = product.Id,
                    DiscountPercentage = 10,
                    DiscountValue = product.SoldPrice * product.Quantity * 0.10m,
                    Reason = "10% discount for purchasing more than 4 identical items",
                    Sale = this,
                    ProductSold = product
                });
                product.SoldPrice = product.Price * 0.90m;
            }
            else if (product.Quantity >= 10 && product.Quantity <= 20)
            {
                Discounts.Add(new SaleDiscount
                {
                    SaleId = this.Id,
                    ProductSoldId = product.Id,
                    DiscountPercentage = 20,
                    DiscountValue = product.SoldPrice * product.Quantity * 0.20m,
                    Reason = "20% discount for purchasing between 10 and 20 identical items",
                    Sale = this,
                    ProductSold = product
                });
                product.SoldPrice = product.Price * 0.80m;
            }
        }

        SetTotalPrice();
    }
}
