using Ambev.DeveloperEvaluation.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a rating for a product.
/// </summary>
[Table("Ratings")]
public class Rating : BaseEntity
{
    /// <summary>
    /// Gets or sets the product ID associated with the rating.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the rating value.
    /// </summary>
    public decimal Rate { get; set; }

    /// <summary>
    /// Gets or sets the count of ratings.
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    /// Gets or sets the product associated with the rating.
    /// </summary>
    public Product Product { get; set; } = null!;
}
