using CarRental.Core.Entities;
using CarRental.Core.Repositories;
using CarRental.Core.Services;
using CarRental.Core.UnitOfWorks;

namespace CarRental.Business.Services;
public class WorkTimeService : Service<WorkTime>, IWorkTimeService
{
    private readonly IWorkTimeRepository _workTimeRepository;

    public WorkTimeService(IWorkTimeRepository workTimeRepository, IUnitOfWork unitOfWork) : base(workTimeRepository, unitOfWork)
    {
        _workTimeRepository = workTimeRepository;
    }

}
