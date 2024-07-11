using Library.Core.Domain;
using Library.Domain.Enum;

namespace Library.Domain.Models
{
    public class Book : BaseEntity
    {
        public Book(string title, string author, string iSBN, int yearOfPublication)
        {
            Title = title;
            Author = author;
            ISBN = iSBN;
            YearOfPublication = yearOfPublication;

            Status = BookStatusEnum.Available;
            Loans = new List<Loan> { };
        }

        public string Title { get; private set; }
        public string Author { get; private set; }
        public string ISBN { get; private set; }
        public int YearOfPublication { get; private set; }
        public BookStatusEnum Status { get; private set; }
        public List<Loan> Loans { get; private set; }
        public void Available() => Status = BookStatusEnum.Available;
        public void Lost() => Status = BookStatusEnum.Lost;
        public void Borrowed() => Status = BookStatusEnum.Borrowed;
        public bool IsBorrowed() => Status == BookStatusEnum.Borrowed ? true : false;
        public bool IsAvailable() => Status == BookStatusEnum.Available ? true : false;
    }
}
