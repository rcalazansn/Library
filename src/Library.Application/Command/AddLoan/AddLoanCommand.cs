using MediatR;

namespace Library.Application.Command.AddLoan
{
    public class AddLoanCommand : IRequest<AddLoanCommandResponse>
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
    }
}
