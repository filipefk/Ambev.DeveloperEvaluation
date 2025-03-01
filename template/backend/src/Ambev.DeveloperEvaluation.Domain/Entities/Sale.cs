using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a sale transaction.
/// </summary>
public class Sale : BaseEntity
{
    /// <summary>
    /// Gets or sets the user ID associated with the sale.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the branch ID where the sale occurred.
    /// </summary>
    public Guid BranchId { get; set; }

    /// <summary>
    /// Gets or sets the sale number.
    /// </summary>
    public long SaleNumber { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the sale occurred.
    /// </summary>
    public DateTime Date { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the date and time when the sale was last updated.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the collection of products sold in the sale.
    /// </summary>
    public ICollection<ProductSold> Products { get; set; } = [];

    /// <summary>
    /// Gets or sets the collection of discounts applied to the sale.
    /// </summary>
    public ICollection<SaleDiscount> Discounts { get; set; } = [];

    /// <summary>
    /// Gets or sets the total amount of the sale.
    /// </summary>
    public decimal SaleTotal { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the sale was canceled.
    /// </summary>
    public bool Canceled { get; set; } = false;

    /// <summary>
    /// Gets or sets the user associated with the sale.
    /// </summary>
    public User User { get; set; } = null!;

    /// <summary>
    /// Gets or sets the branch where the sale occurred.
    /// </summary>
    public Branch Branch { get; set; } = null!;

    /// <summary>
    /// Creates a new sale instance from a shopping cart and branch.
    /// </summary>
    /// <param name="cart">The shopping cart containing the products to be sold.</param>
    /// <param name="branch">The branch where the sale is occurring.</param>
    /// <returns>A new sale instance.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the cart is null.</exception>
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

    /// <summary>
    /// Sets the total price of the sale based on the sold products.
    /// </summary>
    public void SetTotalPrice()
    {
        SaleTotal = Products.Sum(p => p.SoldPrice * p.Quantity);
    }

    /// <summary>
    /// Applies discounts to the sale based on the quantity of products sold.
    /// </summary>
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
