using DeveloperApi.Dtos;
using FluentValidation;

namespace DeveloperApi.Validators;

public class UpdateDeveloperDtoValidator : AbstractValidator<UpdateDeveloperDto>
{
    public UpdateDeveloperDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Ім'я обов'язкове.")
            .MaximumLength(50).WithMessage("Ім'я не може перевищувати 50 символів.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Прізвище обов'язкове.")
            .MaximumLength(50).WithMessage("Прізвище не може перевищувати 50 символів.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email обов'язковий.")
            .EmailAddress().WithMessage("Некоректний формат email.")
            .MaximumLength(100).WithMessage("Email не може перевищувати 100 символів.");

        RuleFor(x => x.Specialization)
            .NotEmpty().WithMessage("Спеціалізація обов'язкова.")
            .MaximumLength(50).WithMessage("Спеціалізація не може перевищувати 50 символів.");

        RuleFor(x => x.Level)
            .IsInEnum().WithMessage("Некоректний рівень розробника.");

        RuleFor(x => x.Salary)
            .GreaterThan(0).WithMessage("Зарплата має бути більшою за 0.")
            .LessThanOrEqualTo(1_000_000).WithMessage("Зарплата вказана некоректно.");

        RuleFor(x => x.HireDate)
            .NotEmpty().WithMessage("Дата найму обов'язкова.")
            .LessThanOrEqualTo(DateTime.Today).WithMessage("Дата найму не може бути в майбутньому.");
    }
}
