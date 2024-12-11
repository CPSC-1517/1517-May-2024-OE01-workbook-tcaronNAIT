using ExampleOOP;
using ExampleWebApp.DTOs;
using FluentValidation;

namespace ExampleWebApp.Components.ViewModels
{
    public class PersonValidationVM : AbstractValidator<PersonDto>
    {
        public PersonValidationVM()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is super required.").MaximumLength(50).WithMessage("First Name cannot be longer than 50 characters.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is super required.");
            RuleFor(x => x.PostalCode).NotEmpty().WithMessage("Postal Code is required.").Matches(@"[A-Z][0-9][A-Z] [0-9][A-Z][0-9]").WithMessage("Postal Code must match the format A0A 0A0.");
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<PersonDto>.CreateWithOptions((PersonDto)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
