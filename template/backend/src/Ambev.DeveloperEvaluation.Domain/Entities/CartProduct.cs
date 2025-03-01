using Ambev.DeveloperEvaluation.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a product in a shopping cart.
/// </summary>
[Table("CartProducts")]
public class CartProduct : BaseEntity
{
    /// <summary>
    /// Gets or sets the product ID.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the cart ID.
    /// </summary>
    public Guid CartId { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the product in the cart.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the product was added to the cart.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the date and time when the product was last updated in the cart.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the product associated with this cart product.
    /// </summary>
    public required Product Product { get; set; }

    /// <summary>
    /// Gets or sets the cart associated with this cart product.
    /// </summary>
    public required Cart Cart { get; set; }
}
