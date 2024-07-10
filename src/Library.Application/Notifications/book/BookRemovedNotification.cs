using Library.Domain.Enum;
using Library.Domain.Models;
using MediatR;

namespace Library.Application.Notifications.book
{
    public class BookRemovedNotification : INotification
    {
        public BookRemovedNotification(Book book)
        {
            Id = book.Id;
            Title = book.Title;
            Author = book.Author;
            ISBN = book.ISBN;
            YearOfPublication = book.YearOfPublication;
            Status = book.Status;
        }

        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Author { get; private set; }
        public string ISBN { get; private set; }
        public int YearOfPublication { get; private set; }
        public BookStatusEnum Status { get; private set; }
    }
}
