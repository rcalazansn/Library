using MediatR;

namespace Library.Application.Command.AddBook
{
    public class AddBookCommand : IRequest
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public int YearOfPublication { get; set; }
    }
}
