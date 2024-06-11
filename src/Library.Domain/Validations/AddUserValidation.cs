using FluentValidation;
using Library.Domain.Models;

namespace Library.Domain.Validations
{
    public class AddUserValidation : AbstractValidator<User>
    {
        public AddUserValidation()
        {
            RuleFor(x => x.Name)
               .NotEmpty()
               .WithMessage("Name is Required");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is Required");
        }
    }
}
