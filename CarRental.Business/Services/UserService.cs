using CarRental.Core.Entities;
using CarRental.Core.Repositories;
using CarRental.Core.Services;
using CarRental.Core.UnitOfWorks;
using FluentValidation;

namespace CarRental.Business.Services;
public class UserService : Service<User>, IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<User> _validator;
    public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, IValidator<User> validator) : base(userRepository, unitOfWork, validator)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<User> AuthenticateAsync(string email, string password)
    {
        return await _userRepository.AuthenticateAsync(email, password);
    }

    public async Task RegisterAsync(User user)
    {
        user.Role = Roles.User;
        await _userRepository.RegisterAsync(user);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<User> GetUserCarsAsync(Guid userId)
    {
        return await _userRepository.GetUserCarsAsync(userId);
    }

    public async Task<User> GetUserWithCarsAndWorkTimesAsync(Guid userId)
    {
        return await _userRepository.GetUserWithCarsAndWorkTimesAsync(userId);
    }
}
