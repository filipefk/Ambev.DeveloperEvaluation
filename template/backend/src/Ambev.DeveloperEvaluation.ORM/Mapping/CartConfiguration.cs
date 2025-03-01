using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class CartConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.ToTable("Carts");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(c => c.UserId).IsRequired();
        builder.Property(c => c.Date).IsRequired().HasColumnType("date");
        builder.Property(c => c.UpdatedAt).IsRequired(false);

        builder.HasMany(c => c.Products)
               .WithOne(p => p.Cart)
               .HasForeignKey(p => p.CartId);

        builder.HasOne(c => c.User)
               .WithMany()
               .HasForeignKey(c => c.UserId);
    }
}
