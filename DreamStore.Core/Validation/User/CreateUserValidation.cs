using DreamStore.Core.Dtos.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamStore.Core.Validation.User
{
    public class CreateUserValidation :AbstractValidator<CreateUserDto>
    {
        public CreateUserValidation()
        {
            RuleFor(x => x.Name)
             .NotNull().NotEmpty().WithMessage("Ім'я не може бути пустим")
             .MaximumLength(25).WithMessage("Максимальна довжина ім'я 25 символів")
             .MinimumLength(2).WithMessage("мінімальна довжина ім'я 2 символа");

            RuleFor(x => x.Surname)
             .NotNull().NotEmpty().WithMessage("Прізвище не може бути пустим")
             .MaximumLength(30).WithMessage("Максимальна довжина Прізвища 30 символів")
             .MinimumLength(4).WithMessage("мінімальна довжина Прізвища 4 символа");

            RuleFor(x => x.Email)
            .NotNull().NotEmpty().WithMessage("Емейл не може бути пустим")
            .MaximumLength(128).WithMessage("Максимальна довжина Емейлу 128 символів")
            .EmailAddress().WithMessage("Email не валідний");

            RuleFor(x => x.PhoneNumber)
            .MaximumLength(15).WithMessage("Максимальна довжина номера телефону 15 символів");

            RuleFor(x => x.Password)
            .NotNull().NotEmpty().WithMessage("Пароль не може бути пустим")
            .MinimumLength(8).WithMessage("Мінімальна довжина паролю 8 символів")
            .MaximumLength(16).WithMessage("Максимальна довжина паролю 16 символів")
            .Matches(@"[A-Z]+").WithMessage("Пароль повинен мати 1 велику літеру")
            .Matches(@"[a-z]+").WithMessage("Пароль повинен мати 1 малу літеру")
            .Matches(@"[0-9]+").WithMessage("Пароль повинен мати 1 цифру");

            RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password).WithMessage("Повинен бути однаковим з паролем");
        }
    }
}
