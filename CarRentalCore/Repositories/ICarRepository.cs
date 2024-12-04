using CarRental.Core.Entities;

namespace CarRental.Core.Repositories;
public interface ICarRepository : IGenericRepository<Car>
{
    Task<List<Car>> GetUserCarsAsync(Guid userId);
}
