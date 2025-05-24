using DreamStore.Core.Dtos.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamStore.Core.Validation.User
{
    public class UpdateUserValidation : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserValidation()
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
        }
    }
}
