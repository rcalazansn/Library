using Library.Domain.Enum;
using Library.Domain.Utils;

namespace Library.Application.ViewModel
{
    public class BookDetailsViewModel
    {
        public BookDetailsViewModel(int id, string title, string author, string iSBN, int yearOfPublication, BookStatusEnum status)
        {
            Id = id;
            Title = title;
            Author = author;
            ISBN = iSBN;
            YearOfPublication = yearOfPublication;
            Status = status.GetDisplayName();
        }

        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Author { get; private set; }
        public string ISBN { get; private set; }
        public int YearOfPublication { get; private set; }
        public string Status { get; private set; }
    }
}
