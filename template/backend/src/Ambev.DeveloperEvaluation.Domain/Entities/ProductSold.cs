using Ambev.DeveloperEvaluation.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

[Table("ProductsSold")]
public class ProductSold : BaseEntity
{
    [Required]
    public Guid SaleId { get; set; }

    [Required]
    public Guid ProductId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    [Required]
    public int Quantity { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal SoldPrice { get; set; }

    [Required]
    [MaxLength(100)]
    public string Category { get; set; } = string.Empty;

    public Sale Sale { get; set; }

    public Product Product { get; set; }
}
