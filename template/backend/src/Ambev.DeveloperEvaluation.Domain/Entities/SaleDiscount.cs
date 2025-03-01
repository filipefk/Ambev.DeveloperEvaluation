using Ambev.DeveloperEvaluation.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a discount applied to a sale.
/// </summary>
[Table("SaleDiscounts")]
public class SaleDiscount : BaseEntity
{
    /// <summary>
    /// Gets or sets the sale ID associated with the discount.
    /// </summary>
    public Guid SaleId { get; set; }

    /// <summary>
    /// Gets or sets the product sold ID associated with the discount.
    /// </summary>
    public required Guid ProductSoldId { get; set; }

    /// <summary>
    /// Gets or sets the discount percentage.
    /// </summary>
    public decimal DiscountPercentage { get; set; }

    /// <summary>
    /// Gets or sets the discount value.
    /// </summary>
    public decimal DiscountValue { get; set; }

    /// <summary>
    /// Gets or sets the reason for the discount.
    /// </summary>
    public string Reason { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the sale associated with the discount.
    /// </summary>
    public required Sale Sale { get; set; }

    /// <summary>
    /// Gets or sets the product sold associated with the discount.
    /// </summary>
    public required ProductSold ProductSold { get; set; }
}
