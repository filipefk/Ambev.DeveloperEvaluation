using Ambev.DeveloperEvaluation.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Cart : BaseEntity
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    [Column(TypeName = "date")]
    public DateTime Date { get; set; } = DateTime.UtcNow;

    public ICollection<CartProduct> Products { get; set; } = [];

    public DateTime? UpdatedAt { get; set; }

    public required User User { get; set; }

    public void AddProduct(CartProduct product)
    {
        var existingProduct = Products.FirstOrDefault(p => p.ProductId == product.ProductId);
        if (existingProduct != null)
        {
            existingProduct.Quantity += product.Quantity;
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
}
