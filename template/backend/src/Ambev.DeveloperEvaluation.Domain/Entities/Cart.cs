using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a shopping cart containing products for a user.
/// </summary>
public class Cart : BaseEntity
{
    /// <summary>
    /// Gets or sets the user ID associated with the cart.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the date when the cart was created.
    /// </summary>
    public DateTime Date { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the collection of products in the cart.
    /// </summary>
    public ICollection<CartProduct> Products { get; set; } = [];

    /// <summary>
    /// Gets or sets the date and time when the cart was last updated.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the user associated with the cart.
    /// </summary>
    public User User { get; set; } = null!;

    /// <summary>
    /// Adds a product to the cart. If the product already exists, its quantity is updated.
    /// </summary>
    /// <param name="product">The product to add to the cart.</param>
    public void AddProduct(CartProduct product)
    {
        var existingProduct = Products.FirstOrDefault(p => p.ProductId == product.ProductId);
        if (existingProduct != null)
        {
            existingProduct.Quantity += product.Quantity;
            existingProduct.UpdatedAt = DateTime.UtcNow;
        }
        else
        {
            Products.Add(product);
        }
    }

    /// <summary>
    /// Consolidates products in the cart by combining quantities of the same product.
    /// </summary>
    public void ConsolidateProducts()
    {
        var distinctProductIds = Products.Select(p => p.ProductId).Distinct().Count();
        var totalProducts = Products.Count;

        if (distinctProductIds < totalProducts)
        {
            var productDictionary = new Dictionary<Guid, CartProduct>();

            foreach (var product in Products)
            {
                if (productDictionary.ContainsKey(product.ProductId))
                {
                    productDictionary[product.ProductId].Quantity += product.Quantity;
                }
                else
                {
                    productDictionary[product.ProductId] = product;
                }
            }

            Products = productDictionary.Values.ToList();
        }
    }

    /// <summary>
    /// Checks if any product in the cart exceeds the specified maximum quantity limit.
    /// </summary>
    /// <param name="limit">The maximum quantity limit per product.</param>
    /// <returns>True if any product exceeds the limit; otherwise, false.</returns>
    public bool ExceedsMaximumQuantityPerProduct(int limit)
    {
        if (limit == 0)
            return false;

        foreach (var product in Products)
        {
            if (product.Quantity > limit)
            {
                return true;
            }
        }
        return false;
    }
}
