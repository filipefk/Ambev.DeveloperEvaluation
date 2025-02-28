using Ambev.DeveloperEvaluation.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Branch : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
