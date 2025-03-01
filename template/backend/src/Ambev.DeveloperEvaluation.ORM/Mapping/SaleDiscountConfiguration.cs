using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class SaleDiscountConfiguration : IEntityTypeConfiguration<SaleDiscount>
{
    public void Configure(EntityTypeBuilder<SaleDiscount> builder)
    {
        builder.ToTable("SaleDiscounts");

        builder.HasKey(sd => sd.Id);
        builder.Property(sd => sd.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(sd => sd.SaleId).HasColumnType("uuid").IsRequired();
        builder.Property(sd => sd.ProductSoldId).HasColumnType("uuid").IsRequired();
        builder.Property(sd => sd.DiscountPercentage).IsRequired();
        builder.Property(sd => sd.DiscountValue).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(sd => sd.Reason).IsRequired().HasMaxLength(255);

        builder.HasOne(sd => sd.Sale)
               .WithMany(s => s.Discounts)
               .HasForeignKey(sd => sd.SaleId);

        builder.HasOne(sd => sd.ProductSold)
               .WithMany()
               .HasForeignKey(sd => sd.ProductSoldId);
    }
}