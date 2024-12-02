using CarRental.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.DataAccess.Configurations;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(w => w.Id);

        builder.Property(c => c.Email)
            .IsRequired();

        builder.HasIndex(c => c.Email)
            .IsUnique();

    }
}
