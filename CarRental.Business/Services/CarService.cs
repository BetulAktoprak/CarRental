using CarRental.Core.Entities;
using CarRental.Core.Repositories;
using CarRental.Core.Services;
using CarRental.Core.UnitOfWorks;
using CarRental.Core.ViewModels;

namespace CarRental.Business.Services;
public class CarService : Service<Car>, ICarService
{
    private readonly ICarRepository _carRepository;

    public CarService(ICarRepository carRepository, IUnitOfWork unitOfWork) : base(carRepository, unitOfWork)
    {
        _carRepository = carRepository;
    }

    public async Task<List<CarWorkTimeViewModel>> GetAdminReportAsync(DateTime startDate, DateTime endDate)
    {
        return await _carRepository.GetAdminReportAsync(startDate, endDate);
    }

    public async Task<List<Car>> GetUserCarsAsync(Guid userId)
    {
        return await _carRepository.GetUserCarsAsync(userId);
    }
}
