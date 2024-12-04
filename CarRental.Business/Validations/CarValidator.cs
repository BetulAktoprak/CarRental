using CarRental.Core.Entities;
using FluentValidation;

namespace CarRental.Business.Validations;
public class CarValidator : AbstractValidator<Car>
{
    public CarValidator()
    {
        RuleFor(car => car.Name)
            .NotEmpty()
            .WithMessage("Araba ismi baş geçilemez.");

        RuleFor(car => car.Plate)
            .NotEmpty().WithMessage("Plaka boş olamaz.")
            .Matches(@"^\d{2}\s?[A-Z]{1,3}\s?\d{3,4}$")
            .WithMessage("Plaka formatı geçersiz. Geçerli formatlar: 99 X 9999, 99 XX 999, 99 XXX 99, 99 XXX 999");

        RuleFor(car => car.Price)
            .GreaterThan(-1).WithMessage("Fiyat negatif olamaz.");

        RuleFor(car => car.UserId)
            .NotEqual(Guid.Empty).WithMessage("Geçerli bir kullanıcı giriniz.");
    }
}
