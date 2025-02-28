using Ambev.DeveloperEvaluation.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Sale : BaseEntity
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    public Guid BranchId { get; set; }

    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long SaleNumber { get; set; }

    [Required]
    [Column(TypeName = "date")]
    public DateTime Date { get; set; } = DateTime.UtcNow;

    public ICollection<ProductSold> Products { get; set; } = [];

    public ICollection<SaleDiscount> Discounts { get; set; } = [];

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal SaleTotal { get; set; }

    [Required]
    public bool Canceled { get; set; } = false;

    public required User User { get; set; }

    public required Branch Branch { get; set; }
}
