using Library.Domain.Enum;
using Library.Domain.Utils;

namespace Library.Application.ViewModel
{
    public class BookViewModel
    {
        public BookViewModel(int id, string title, string author, string iSBN, int yearOfPublication, BookStatusEnum status, bool isBorrowed)
        {
            Id = id;
            Title = title;
            Author = author;
            ISBN = iSBN;
            YearOfPublication = yearOfPublication;
            Status = status.GetDisplayName();
            IsBorrowed = isBorrowed;
        }
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Author { get; private set; }
        public string ISBN { get; private set; }
        public int YearOfPublication { get; private set; }
        public string Status { get; private set; }
        public bool IsBorrowed { get; set; }
    }
}
