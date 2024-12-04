using CarRental.Core.Entities;
using FluentValidation;

namespace CarRental.Business.Validations;
public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(user => user.FullName)
             .NotEmpty().WithMessage("Ad ve soyad boş bırakılamaz.")
             .MinimumLength(2).WithMessage("Ad ve soyad en az 2 karakter uzunluğunda olmalıdır.");

        RuleFor(user => user.UserName)
            .NotEmpty().WithMessage("Kullanıcı adı boş bırakılamaz.")
            .MinimumLength(3).WithMessage("Kullanıcı adı en az 3 karakter uzunluğunda olmalıdır.");

        RuleFor(user => user.Email)
            .NotEmpty().WithMessage("E-posta adresi boş bırakılamaz.")
            .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");

        RuleFor(user => user.Password)
            .NotEmpty().WithMessage("Şifre boş bırakılamaz.")
            .MinimumLength(6).WithMessage("Şifre en az 6 karakter uzunluğunda olmalıdır.");

    }
}
