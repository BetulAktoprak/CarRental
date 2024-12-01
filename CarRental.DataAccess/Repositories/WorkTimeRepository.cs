using CarRental.Core.Entities;
using CarRental.Core.Repositories;
using CarRental.DataAccess.Context;

namespace CarRental.DataAccess.Repositories;
public class WorkTimeRepository : GenericRepository<WorkTime>, IWorkTimeRepository
{
    private readonly AppDbContext _context;
    public WorkTimeRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

}
