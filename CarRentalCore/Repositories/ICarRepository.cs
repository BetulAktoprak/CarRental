using CarRental.Core.Entities;
using CarRental.Core.ViewModels;

namespace CarRental.Core.Repositories;
public interface ICarRepository : IGenericRepository<Car>
{
    Task<List<CarWorkTimeViewModel>> GetAdminReportAsync(DateTime startDate, DateTime endDate);
    Task<List<Car>> GetUserCarsAsync(Guid userId);
}
