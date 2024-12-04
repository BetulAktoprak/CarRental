using CarRental.Core.Entities;

namespace CarRental.Core.Services;
public interface ICarService : IService<Car>
{
    Task<List<Car>> GetUserCarsAsync(Guid userId);
}
