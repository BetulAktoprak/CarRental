using CarRental.Core.Entities;
using CarRental.Core.Repositories;
using CarRental.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace CarRental.DataAccess.Repositories;
public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly AppDbContext _context;
    public UserRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<User> AuthenticateAsync(string email, string password)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u =>
                u.Email == email &&
                u.Password == password);
    }

    public async Task<User> GetUserCarsAsync(Guid userId)
    {
        return await _context.Users
            .Include(c => c.Cars)
            .FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task<User> GetUserWithCarsAndWorkTimesAsync(Guid userId)
    {
        return await _context.Users
            .Include(u => u.Cars)
                .ThenInclude(c => c.WorkTimes)
            .FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task RegisterAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }
}
