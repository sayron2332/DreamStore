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
            RuleFor(u => u.Password)
               .NotEmpty().WithMessage("Пароль не може бути пустим")
               .MinimumLength(6).WithMessage("Мінімальна довжина паролю 6 символів")
               .MaximumLength(20).WithMessage("Максимальна довжина паролю 20 символів");
            RuleFor(u => u.ConfirmPassword).Equal(u => u.Password)
                .WithMessage("Паролі мають бути однаковими");
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
