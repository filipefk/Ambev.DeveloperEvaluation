using Ambev.DeveloperEvaluation.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

[Table("Ratings")]
public class Rating : BaseEntity
{
    [Required]
    public Guid ProductId { get; set; }

    [Column(TypeName = "decimal(3,2)")]
    public decimal Rate { get; set; }

    [Required]
    public int Count { get; set; }

    public Product Product { get; set; }
}
