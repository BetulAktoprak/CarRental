using CarRental.Core.Entities;
using FluentValidation;

namespace CarRental.Business.Validations;
public class WorkTimeValidator : AbstractValidator<WorkTime>
{
    public WorkTimeValidator()
    {
    }
}
