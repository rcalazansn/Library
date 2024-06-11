using FluentValidation;
using Library.Domain.Models;

namespace Library.Domain.Validations
{
    public class ReturnBookValidation : AbstractValidator<Loan>
    {
        public ReturnBookValidation()
        {
            RuleFor(_ => _.Id)
                .LessThan(0)
                .WithMessage("Id is Required");
        }
    }
}
