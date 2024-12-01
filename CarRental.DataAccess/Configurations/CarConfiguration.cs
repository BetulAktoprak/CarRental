using CarRental.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.DataAccess.Configurations;
public sealed class CarConfiguration : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.ToTable("Cars");

        builder.HasOne(w => w.User)
               .WithMany(c => c.Cars)
               .HasForeignKey(w => w.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(w => w.UserId);

        builder.Property(c => c.Plate)
            .IsRequired();

        builder.HasIndex(c => c.Plate)
            .IsUnique();

        builder.HasKey(x => x.Id);

        builder.Property(c => c.Price)
            .HasColumnType("decimal(18, 2)");
    }
}
