using CarRental.Core.Entities;
using CarRental.Core.Repositories;
using CarRental.Core.Services;
using CarRental.Core.UnitOfWorks;
using FluentValidation;

namespace CarRental.Business.Services;
public class CarService : Service<Car>, ICarService
{
    private readonly ICarRepository _carRepository;
    private readonly IValidator<Car> _validator;

    public CarService(ICarRepository carRepository, IUnitOfWork unitOfWork, IValidator<Car> validator) : base(carRepository, unitOfWork, validator)
    {
        _carRepository = carRepository;
        _validator = validator;
    }

    public async Task<List<Car>> GetUserCarsAsync(Guid userId)
    {
        return await _carRepository.GetUserCarsAsync(userId);
    }
}
