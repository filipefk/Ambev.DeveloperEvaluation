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
}
