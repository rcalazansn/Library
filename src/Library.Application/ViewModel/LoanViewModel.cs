namespace Library.Application.ViewModel
{
    public class LoanViewModel
    {
        public LoanViewModel
        (
            int id, 
            string name, 
            string title, 
            DateTime loanDate, 
            DateTime deadlineReturnDate, 
            DateTime returnDate
        )
        {
            Id = id;
            Name = name;
            Title = title;
            LoanDate = loanDate;
            DeadlineReturnDate = deadlineReturnDate;

            if (ReturnDate != default(DateTime))
                ReturnDate = returnDate;
            else
                ReturnDate = null;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Title { get; private set; }
        public DateTime LoanDate { get; private set; }
        public DateTime DeadlineReturnDate { get; private set; }
        public DateTime? ReturnDate { get; private set; }
    }
}
