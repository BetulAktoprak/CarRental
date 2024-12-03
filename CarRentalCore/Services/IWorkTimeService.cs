using CarRental.Core.Entities;
using System.Linq.Expressions;

namespace CarRental.Core.Services;
public interface IWorkTimeService : IService<WorkTime>
{
    Task<List<WorkTime>> GetAllWithFilterAsync(Expression<Func<WorkTime, bool>> filter);
}
