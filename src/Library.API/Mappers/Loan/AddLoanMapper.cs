using Library.API.Dtos.Loan;
using Library.Application.Command.AddLoan;

namespace Library.API.Mappers.Loan
{
    public static class AddLoanMapper
    {
        public static AddLoanCommand MapToAddLoanCommand(this AddLoanRequestDto dto)
        {
            return new AddLoanCommand()
            {
                BookId = dto.BookId,
                UserId = dto.UserId,
            };
        }
    }
}
