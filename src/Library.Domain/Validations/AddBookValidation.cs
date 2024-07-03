using FluentValidation;
using Library.Domain.Models;

namespace Library.Domain.Validations
{
    public class AddBookValidation : AbstractValidator<Book>
    {
        public AddBookValidation()
        {
            RuleFor(x => x.Title)
               .NotEmpty()
               .WithMessage("is Required");

            RuleFor(x => x.Author)
                .NotEmpty()
                .WithMessage("is Required");

            RuleFor(x => x.ISBN)
                .NotEmpty()
                .WithMessage("is Required");

            RuleFor(x => x.YearOfPublication)
                .GreaterThan(1986)
                .WithMessage("should be greater than 1986 ");
        }
    }
}
