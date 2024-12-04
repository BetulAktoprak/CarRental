using CarRental.Core.Entities;
using FluentValidation;

namespace CarRental.Business.Validations;
public class WorkTimeValidator : AbstractValidator<WorkTime>
{
    public WorkTimeValidator()
    {

        RuleFor(workTime => workTime.ActiveWorkHours)
            .GreaterThanOrEqualTo(0).WithMessage("Aktif çalışma saatleri sıfırdan küçük olamaz.");

        RuleFor(workTime => workTime.MaintenanceHours)
            .GreaterThanOrEqualTo(0).WithMessage("Bakım saatleri sıfırdan küçük olamaz.");

    }
}
