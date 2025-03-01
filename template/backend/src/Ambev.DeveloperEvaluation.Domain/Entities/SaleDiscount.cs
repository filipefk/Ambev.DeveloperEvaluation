using Ambev.DeveloperEvaluation.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

[Table("SaleDiscounts")]
public class SaleDiscount : BaseEntity
{
    public Guid SaleId { get; set; }

    public required Guid ProductSoldId { get; set; }

    public decimal DiscountPercentage { get; set; }

    public decimal DiscountValue { get; set; }

    public string Reason { get; set; } = string.Empty;

    public required Sale Sale { get; set; }

    public required ProductSold ProductSold { get; set; }
}
