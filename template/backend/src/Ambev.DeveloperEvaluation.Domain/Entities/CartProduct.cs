using Ambev.DeveloperEvaluation.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

[Table("CartProducts")]
public class CartProduct : BaseEntity
{
    public Guid ProductId { get; set; }

    public Guid CartId { get; set; }

    public int Quantity { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public required Product Product { get; set; }

    public required Cart Cart { get; set; }
}
