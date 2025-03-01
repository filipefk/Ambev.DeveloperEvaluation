using Ambev.DeveloperEvaluation.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

[Table("ProductsSold")]
public class ProductSold : BaseEntity
{
    public Guid SaleId { get; set; }

    public Guid ProductId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public decimal SoldPrice { get; set; }

    public string Category { get; set; } = string.Empty;

    public required Sale Sale { get; set; }

    public required Product Product { get; set; }
}
