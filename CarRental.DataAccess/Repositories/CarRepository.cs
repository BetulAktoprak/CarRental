using CarRental.Core.Entities;
using CarRental.Core.Repositories;
using CarRental.Core.ViewModels;
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

    public async Task<List<CarWorkTimeViewModel>> GetAdminReportAsync(DateTime startDate, DateTime endDate)
    {
        const int hoursInAWeek = 7 * 24;

        return await _context.Cars
            .Include(c => c.WorkTimes)
            .Where(c => c.WorkTimes.Any(w => w.RecordedDate >= startDate && w.RecordedDate <= endDate))
            .Select(c => new CarWorkTimeViewModel
            {
                Name = c.Name,
                Plate = c.Plate,
                ActiveWorkHours = c.WorkTimes
                    .Where(w => w.RecordedDate >= startDate && w.RecordedDate <= endDate)
                    .Sum(w => w.ActiveWorkHours),
                MaintenanceHours = c.WorkTimes
                    .Where(w => w.RecordedDate >= startDate && w.RecordedDate <= endDate)
                    .Sum(w => w.MaintenanceHours),
                IdleTime = hoursInAWeek - c.WorkTimes
                    .Where(w => w.RecordedDate >= startDate && w.RecordedDate <= endDate)
                    .Sum(w => w.ActiveWorkHours + w.MaintenanceHours)
            })
            .ToListAsync();
    }
}
