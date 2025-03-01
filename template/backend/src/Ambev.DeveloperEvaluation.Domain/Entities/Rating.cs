using Ambev.DeveloperEvaluation.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

[Table("Ratings")]
public class Rating : BaseEntity
{
    public Guid ProductId { get; set; }

    public decimal Rate { get; set; }

    public int Count { get; set; }

    public required Product Product { get; set; }
}
