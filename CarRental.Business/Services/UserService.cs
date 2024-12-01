using CarRental.Core.Entities;
using CarRental.Core.Repositories;
using CarRental.Core.Services;
using CarRental.Core.UnitOfWorks;
using System.Data;

namespace CarRental.Business.Services;
public class UserService : Service<User>, IUserService
{
    private readonly IUserRepository _userRepository;
    public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork) : base(userRepository, unitOfWork)
    {
        _userRepository = userRepository;
    }

    private readonly List<User> _users = new()
    {
        new User { FullName = "Admin User", UserName = "admin",Email = "admin@gmail.com", Password = "admin123", Role = Roles.Admin }
    };

    private readonly List<Car> _cars = new();

    public async Task<User> AuthenticateAsync(string username, string email, string password)
    {
        return _users.FirstOrDefault(u => u.UserName == username && u.Password == password && u.Email == email);
    }

    public async Task RegisterAsync(User user)
    {
        user.Role = Roles.User;
        _users.Add(user);
    }

    public async Task<List<Car>> GetUserCarsAsync(Guid userId)
    {
        return _cars.Where(c => c.UserId == userId).ToList();
    }
}
