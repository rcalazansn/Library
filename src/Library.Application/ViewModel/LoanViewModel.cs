using Library.Domain.Enum;
using Library.Domain.Utils;

namespace Library.Application.ViewModel
{
    public class LoanViewModel
    {
        public LoanViewModel
        (
            int id,
            int userId,
            int bookId,
            string name, 
            string title, 
            DateTime loanDate, 
            DateTime deadlineReturnDate, 
            DateTime returnDate,
            BookStatusEnum bookStatusEnum
        )
        {
            Id = id;
            UserId = userId;
            BookId = bookId;
            Name = name;
            Title = title;
            LoanDate = loanDate;
            DeadlineReturnDate = deadlineReturnDate;
            ReturnDate = returnDate;

            BookStatusEnum = bookStatusEnum.GetDisplayName();
        }

        public int Id { get; private set; }
        public int UserId { get; private set; }
        public int BookId { get; private set; }
        public string Name { get; private set; }
        public string Title { get; private set; }
        public DateTime LoanDate { get; private set; }
        public DateTime DeadlineReturnDate { get; private set; }
        public DateTime? ReturnDate { get; private set; }
        public string BookStatusEnum { get; private set; }
    }
}
