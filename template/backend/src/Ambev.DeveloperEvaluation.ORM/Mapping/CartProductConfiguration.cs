using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class CartProductConfiguration : IEntityTypeConfiguration<CartProduct>
{
    public void Configure(EntityTypeBuilder<CartProduct> builder)
    {
        builder.ToTable("CartProducts");

        builder.HasKey(cp => cp.Id);
        builder.Property(cp => cp.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(cp => cp.ProductId).HasColumnType("uuid").IsRequired();
        builder.Property(cp => cp.CartId).HasColumnType("uuid").IsRequired();
        builder.Property(cp => cp.Quantity).IsRequired();
        builder.Property(cp => cp.CreatedAt).IsRequired();
        builder.Property(cp => cp.UpdatedAt).IsRequired(false);

        builder.HasOne(cp => cp.Product)
               .WithMany()
               .HasForeignKey(cp => cp.ProductId);

        builder.HasOne(cp => cp.Cart)
               .WithMany(c => c.Products)
               .HasForeignKey(cp => cp.CartId);
    }
}

