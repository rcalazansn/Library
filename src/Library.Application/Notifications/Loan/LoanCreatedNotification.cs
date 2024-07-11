using MediatR;

namespace Library.Application.Notifications.Loan
{
    public class LoanCreatedNotification : INotification
    {
        public LoanCreatedNotification(Domain.Models.Loan loan)
        {
            Id = loan.Id;
            User = loan.User;
            UserId = loan.UserId;
            Book = loan.Book;
            BookId = loan.BookId;
            LoanDate = loan.LoanDate;
            DeadlineReturnDate = loan.DeadlineReturnDate;
        }

        public int Id { get; private set; }
        public Domain.Models.User User { get; private set; }
        public int UserId { get; private set; }
        public Domain.Models.Book Book { get; private set; }
        public int BookId { get; private set; }
        public DateTime LoanDate { get; private set; }
        public DateTime DeadlineReturnDate { get; private set; }
    }
}
