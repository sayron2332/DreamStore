using DreamStore.Core.Dtos.Category;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamStore.Core.Validation.Category
{
    public class CategoryValidation : AbstractValidator<CategoryDto>
    {
        public CategoryValidation()
        {
            RuleFor(u => u.Name)
              .NotEmpty().WithMessage("ім'я не може бути пустим")
              .MinimumLength(3).WithMessage("Мінімальна довжина ім'я 3 символа")
              .MaximumLength(20).WithMessage("Максимальна довжина ім'я 20 символів");
        }
    }
}
