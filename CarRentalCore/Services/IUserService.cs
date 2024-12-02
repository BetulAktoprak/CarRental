using CarRental.Core.Entities;

namespace CarRental.Core.Services;
public interface IUserService : IService<User>
{
    Task<User> AuthenticateAsync(string username, string email, string password);
    Task RegisterAsync(User user);
    Task<User> GetUserCarsAsync(Guid userId);
    Task<User> GetUserWithCarsAndWorkTimesAsync(Guid userId);
}
