using CarRental.Core.Entities;
using CarRental.Core.Repositories;
using CarRental.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CarRental.DataAccess.Repositories;
public class WorkTimeRepository : GenericRepository<WorkTime>, IWorkTimeRepository
{
    private readonly AppDbContext _context;
    public WorkTimeRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<WorkTime>> GetAllWithFilterAsync(Expression<Func<WorkTime, bool>> filter)
    {
        return await _context.WorkTimes
            .Include(wt => wt.Car)
            .Where(filter)
            .ToListAsync();

    }
}
