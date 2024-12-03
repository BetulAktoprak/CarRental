using CarRental.Core.Entities;
using System.Linq.Expressions;

namespace CarRental.Core.Repositories;
public interface IWorkTimeRepository : IGenericRepository<WorkTime>
{
    Task<List<WorkTime>> GetAllWithFilterAsync(Expression<Func<WorkTime, bool>> filter);
}
