using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sales");

        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(s => s.UserId).HasColumnType("uuid").IsRequired();
        builder.Property(s => s.BranchId).HasColumnType("uuid").IsRequired();
        builder.Property(s => s.SaleNumber).IsRequired().ValueGeneratedOnAdd();
        builder.Property(s => s.Date).IsRequired().HasColumnType("date");
        builder.Property(s => s.UpdatedAt).IsRequired(false);
        builder.Property(s => s.SaleTotal).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(s => s.Canceled).IsRequired();

        builder.HasMany(s => s.Products)
               .WithOne(ps => ps.Sale)
               .HasForeignKey(ps => ps.SaleId);

        builder.HasMany(s => s.Discounts)
               .WithOne(sd => sd.Sale)
               .HasForeignKey(sd => sd.SaleId);

        builder.HasOne(s => s.User)
               .WithMany()
               .HasForeignKey(s => s.UserId);

        builder.HasOne(s => s.Branch)
               .WithMany()
               .HasForeignKey(s => s.BranchId);
    }
}