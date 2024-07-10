using FluentValidation;
using FluentValidation.Validators;
using Library.Domain.Models;

namespace Library.Domain.Validations
{
    public class AddUserValidation : AbstractValidator<User>
    {
        public AddUserValidation()
        {
            RuleFor(x => x.Name)
               .NotEmpty()
               .WithMessage("is Required");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("is Required");

            RuleFor(x => x.Email)
                .EmailAddress(EmailValidationMode.AspNetCoreCompatible)
                .WithMessage("is not valid");
        }
    }
}
