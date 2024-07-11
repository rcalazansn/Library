using FluentValidation;
using Library.Domain.Models;

namespace Library.Domain.Validations
{
    public class AddLoanValidation : AbstractValidator<Loan>
    {
        public AddLoanValidation()
        {
            RuleFor(x => x.UserId)
              .NotEmpty()
              .WithMessage("is Required");

            RuleFor(x => x.BookId)
              .NotEmpty()
              .WithMessage("is Required");
        }
    }
}
