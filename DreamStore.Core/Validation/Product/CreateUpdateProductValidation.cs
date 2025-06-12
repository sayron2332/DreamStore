using DreamStore.Core.Dtos.Product;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamStore.Core.Validation.Product
{
    public class CreateUpdateProductValidation : AbstractValidator<CreateUpdateProductDto>
    {
        public CreateUpdateProductValidation()
        {
            RuleFor(a => a.Name)
           .MaximumLength(50).WithMessage("Максимальна довжина ім'я 50 символів")
           .MinimumLength(3).WithMessage("Мінімальна довжина ім'я 3 символа");

            RuleFor(x => x.Description)
           .NotNull().NotEmpty().WithMessage("Опис не може бути пустим")
           .MaximumLength(300).WithMessage("Максимальна довжина Опису 300 символів")
           .MinimumLength(4).WithMessage("мінімальна довжина Опису 4 символа");

            RuleFor(x => x.Price)
           .NotNull().NotEmpty().WithMessage("Ціна Не може бути пустим")
           .LessThan(2147483647).WithMessage("Максимальна ціна");

            RuleFor(x => x.CategoryId)
           .NotNull().NotEmpty().WithMessage("Категорія Не може бути пуста");
        }
    }
}
