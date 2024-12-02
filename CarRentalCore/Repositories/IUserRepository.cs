using CarRental.Core.Entities;

namespace CarRental.Core.Repositories;
public interface IUserRepository : IGenericRepository<User>
{
    Task<User> AuthenticateAsync(string username, string email, string password);
    Task RegisterAsync(User user);
    Task<List<Car>> GetUserCarsAsync(Guid userId);
    Task<User> GetUserWithCarsAndWorkTimesAsync(Guid userId);
}
