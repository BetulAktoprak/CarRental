using FluentValidation;

namespace CarRental.Business.Validations;
public class LoginValidator : AbstractValidator<(string email, string password)>
{
    public LoginValidator()
    {
        RuleFor(x => x.email)
            .NotEmpty().WithMessage("E-posta adresi boş bırakılamaz.")
            .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");

        RuleFor(x => x.password)
            .NotEmpty().WithMessage("Şifre boş bırakılamaz.")
            .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır.");
    }
}
