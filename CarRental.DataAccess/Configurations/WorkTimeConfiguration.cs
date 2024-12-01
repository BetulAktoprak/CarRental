using CarRental.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.DataAccess.Configurations;
public class WorkTimeConfiguration : IEntityTypeConfiguration<WorkTime>
{
    public void Configure(EntityTypeBuilder<WorkTime> builder)
    {
        builder.ToTable("WorkTimes");

        builder.HasKey(w => w.Id);

        builder.HasOne(w => w.Car)
               .WithMany(c => c.WorkTimes)
               .HasForeignKey(w => w.CarId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(w => w.CarId);

        builder.Property(w => w.IdleTime)
            .HasColumnType("decimal(18, 1)");

        builder.Property(w => w.ActiveWorkHours)
            .HasColumnType("decimal(18, 1)");

        builder.Property(w => w.MaintenanceHours)
            .HasColumnType("decimal(18, 1)");
    }
}
