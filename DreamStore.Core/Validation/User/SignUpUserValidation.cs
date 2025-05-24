using DreamStore.Core.Dtos.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamStore.Core.Validation.User
{
    public class SignUpUserValidation : AbstractValidator<SignUpUserDto>
    {
        public SignUpUserValidation()
        {
            RuleFor(u => u.Email)
              .NotEmpty().WithMessage("Емейл не може бути пустим")
              .EmailAddress().WithMessage("Поганий формат");

            RuleFor(x => x.Password)
              .NotNull().NotEmpty().WithMessage("Пароль не може бути пустим")
              .MinimumLength(8).WithMessage("Мінімальна довжина паролю 8 символів")
              .MaximumLength(16).WithMessage("Максимальна довжина паролю 16 символів")
              .Matches(@"[A-Z]+").WithMessage("Пароль повинен мати 1 велику літеру")
              .Matches(@"[a-z]+").WithMessage("Пароль повинен мати 1 малу літеру")
              .Matches(@"[0-9]+").WithMessage("Пароль повинен мати 1 цифру");

            RuleFor(u => u.ConfirmPassword)
                .Equal(u => u.Password).WithMessage("Паролі мають бути однаковими");

            RuleFor(u => u.Name)
               .NotEmpty().WithMessage("ім'я не може бути пустим")
               .MinimumLength(3).WithMessage("Мінімальна довжина ім'я 3 символа")
               .MaximumLength(20).WithMessage("Максимальна довжина ім'я 20 символів");

            RuleFor(u => u.Surname)
             .NotEmpty().WithMessage("Прізвище не може бути пустим")
             .MinimumLength(4).WithMessage("Мінімальна довжина Прізвища 4 символа")
             .MaximumLength(30).WithMessage("Максимальна довжина Прізвища 30 символів");
        }
        
    }
}
