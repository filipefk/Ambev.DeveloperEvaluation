using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class ProductSoldConfiguration : IEntityTypeConfiguration<ProductSold>
{
    public void Configure(EntityTypeBuilder<ProductSold> builder)
    {
        builder.ToTable("ProductsSold");

        builder.HasKey(ps => ps.Id);
        builder.Property(ps => ps.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(ps => ps.SaleId).HasColumnType("uuid").IsRequired();
        builder.Property(ps => ps.ProductId).HasColumnType("uuid").IsRequired();
        builder.Property(ps => ps.Title).IsRequired().HasMaxLength(100);
        builder.Property(ps => ps.Description).IsRequired().HasMaxLength(500);
        builder.Property(ps => ps.Quantity).IsRequired();
        builder.Property(ps => ps.Price).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(ps => ps.SoldPrice).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(ps => ps.Category).IsRequired().HasMaxLength(100);

        builder.HasOne(ps => ps.Sale)
               .WithMany(s => s.Products)
               .HasForeignKey(ps => ps.SaleId);

        builder.HasOne(ps => ps.Product)
               .WithMany()
               .HasForeignKey(ps => ps.ProductId);
    }
}


