﻿using Ambev.DeveloperEvaluation.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Cart : BaseEntity
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    public DateTime Date { get; set; } = DateTime.UtcNow;

    public ICollection<CartProduct> Products { get; set; } = [];

    public DateTime? UpdatedAt { get; set; }

    public User User { get; set; }
}
