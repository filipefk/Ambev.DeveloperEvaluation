using Ambev.DeveloperEvaluation.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

[Table("CartProducts")]
public class CartProduct : BaseEntity
{
    [Required]
    public Guid ProductId { get; set; }

    [Required]
    public Guid CartId { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public Product Product { get; set; }

    public Cart Cart { get; set; }
}
