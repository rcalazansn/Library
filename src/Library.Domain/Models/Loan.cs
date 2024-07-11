using Library.Core.Domain;

namespace Library.Domain.Models
{
    public class Loan : BaseEntity
    {
        public Loan(int userId, int bookId)
        {
            UserId = userId;
            BookId = bookId;

            LoanDate = DateTime.Now;
            DeadlineReturnDate = DateTime.Now.AddDays(7);
        }

        public User User { get; private set; }
        public int UserId { get; private set; }
        public Book Book { get; private set; }
        public int BookId { get; private set; }
        public DateTime LoanDate { get; private set; }
        public DateTime DeadlineReturnDate { get; private set; }
        public DateTime ReturnDate { get; private set; }
        public void ReturnBook() =>
            ReturnDate = DateTime.Now;
    }
}
