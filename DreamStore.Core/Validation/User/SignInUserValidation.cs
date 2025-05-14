using DreamStore.Core.Dtos.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamStore.Core.Validation.User
{
    public class SignInUserValidation : AbstractValidator<SignInUserDto>
    {
        public SignInUserValidation()
        {
            RuleFor(u => u.Email)
               .NotEmpty().WithMessage("Емейл не може бути пустим")
               .EmailAddress().WithMessage("Поганий формат");
            RuleFor(u => u.Password)
               .NotEmpty().WithMessage("Пароль не може бути пустим")
               .MinimumLength(6).WithMessage("Мінімальна довжина паролю 6 символів")
               .MaximumLength(20).WithMessage("Максимальна довжина паролю  20 симолів");

        }
    }
}
