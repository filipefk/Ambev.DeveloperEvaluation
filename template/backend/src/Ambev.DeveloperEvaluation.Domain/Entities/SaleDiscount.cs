using Ambev.DeveloperEvaluation.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

[Table("SaleDiscounts")]
public class SaleDiscount : BaseEntity
{
    [Required]
    public Guid SaleId { get; set; }

    public required Guid ProductSoldId { get; set; }

    [Required]
    [Range(0, 100)]
    public decimal DiscountPercentage { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal DiscountValue { get; set; }

    [Required]
    [MaxLength(255)]
    public string Reason { get; set; } = string.Empty;

    public required Sale Sale { get; set; }

    public required ProductSold ProductSold { get; set; }
}
