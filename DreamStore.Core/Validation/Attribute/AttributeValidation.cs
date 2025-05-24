using DreamStore.Core.Dtos.Attribute;
using FluentValidation;


namespace DreamStore.Core.Validation.Attribute
{
    public class AttributeValidation : AbstractValidator<AttributeDto>
    {
        public AttributeValidation()
        {
            RuleFor(u => u.Name)
              .NotEmpty().WithMessage("ім'я не може бути пустим")
              .MinimumLength(3).WithMessage("Мінімальна довжина ім'я 3 символа")
              .MaximumLength(20).WithMessage("Максимальна довжина ім'я 20 символів");
        }
    }
}
