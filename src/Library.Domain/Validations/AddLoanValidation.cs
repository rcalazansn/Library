using FluentValidation;
using Library.Domain.Models;

namespace Library.Domain.Validations
{
    public class AddLoanValidation : AbstractValidator<Loan>
    {
        public AddLoanValidation()
        {
        }
    }
}
