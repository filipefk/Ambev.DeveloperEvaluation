using Ambev.DeveloperEvaluation.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Cart : BaseEntity
{
    public Guid UserId { get; set; }

    [Column(TypeName = "date")]
    public DateTime Date { get; set; } = DateTime.UtcNow;

    public ICollection<CartProduct> Products { get; set; } = [];

    public DateTime? UpdatedAt { get; set; }

    public User User { get; set; } = null!;

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
