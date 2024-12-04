using CarRental.Core.Entities;
using CarRental.Core.Repositories;
using CarRental.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace CarRental.DataAccess.Repositories;
public class CarRepository : GenericRepository<Car>, ICarRepository
{
    private readonly AppDbContext _context;
    public CarRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Car>> GetUserCarsAsync(Guid userId)
    {
        return await _context.Cars
        .Where(c => c.UserId == userId)
        .ToListAsync();
    }
}
