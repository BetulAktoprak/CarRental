using CarRental.Core.Entities;
using FluentValidation;

namespace CarRental.Business.Validations;
public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
    }
}
