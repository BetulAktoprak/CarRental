using CarRental.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRental.DataAccess.Context;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Car> Cars { get; set; }
    public DbSet<WorkTime> WorkTimes { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
