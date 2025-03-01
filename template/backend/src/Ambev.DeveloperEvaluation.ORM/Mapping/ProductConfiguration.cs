using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(p => p.Title).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Price).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(p => p.Description).IsRequired().HasMaxLength(500);
        builder.Property(p => p.Category).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Image).HasMaxLength(255);
        builder.Property(p => p.CreatedAt).IsRequired();
        builder.Property(p => p.UpdatedAt).IsRequired(false);

        builder.HasOne(p => p.Rating)
               .WithOne(r => r.Product)
               .HasForeignKey<Rating>(r => r.ProductId);
    }
}


