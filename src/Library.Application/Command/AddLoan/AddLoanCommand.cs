using MediatR;

namespace Library.Application.Command.AddLoan
{
    public class AddLoanCommand : IRequest
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public DateTime DeadlineReturnDate { get; set; }
    }
}
