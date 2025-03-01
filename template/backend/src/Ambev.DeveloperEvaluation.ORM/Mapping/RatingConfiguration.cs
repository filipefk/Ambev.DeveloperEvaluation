using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class RatingConfiguration : IEntityTypeConfiguration<Rating>
{
    public void Configure(EntityTypeBuilder<Rating> builder)
    {
        builder.ToTable("Ratings");

        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(r => r.ProductId).HasColumnType("uuid").IsRequired();
        builder.Property(r => r.Rate).HasColumnType("decimal(3,2)");
        builder.Property(r => r.Count).IsRequired();

        builder.HasOne(r => r.Product)
               .WithOne(p => p.Rating)
               .HasForeignKey<Rating>(r => r.ProductId);
    }
}