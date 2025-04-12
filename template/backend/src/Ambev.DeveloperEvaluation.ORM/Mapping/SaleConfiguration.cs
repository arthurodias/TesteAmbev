using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

/// <summary>
/// Configures the Sale entity mapping for the database schema.
/// </summary>
public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sales");

        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(s => s.SaleNumber)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(s => s.Date)
            .IsRequired();

        builder.Property(s => s.Customer)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.Branch)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.TotalAmount)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(s => s.IsCancelled)
            .IsRequired();

        builder.HasMany(s => s.Items)
               .WithOne()
               .HasForeignKey(i => i.SaleId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}