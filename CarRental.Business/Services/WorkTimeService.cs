using CarRental.Core.Entities;
using CarRental.Core.Repositories;
using CarRental.Core.Services;
using CarRental.Core.UnitOfWorks;
using FluentValidation;
using System.Linq.Expressions;

namespace CarRental.Business.Services;
public class WorkTimeService : Service<WorkTime>, IWorkTimeService
{
    private readonly IWorkTimeRepository _workTimeRepository;
    private readonly IValidator<WorkTime> _validator;

    public WorkTimeService(IWorkTimeRepository workTimeRepository, IUnitOfWork unitOfWork, IValidator<WorkTime> validator) : base(workTimeRepository, unitOfWork, validator)
    {
        _workTimeRepository = workTimeRepository;
        _validator = validator;
    }

    public async Task<List<WorkTime>> GetAllWithFilterAsync(Expression<Func<WorkTime, bool>> filter)
    {
        return await _workTimeRepository.GetAllWithFilterAsync(filter);
    }
}
