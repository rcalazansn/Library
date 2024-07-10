using Library.API.Dtos.Book;
using Library.Application.Command.AddBook;

namespace Library.API.Mappers.Book
{
    public static class AddBookMapper
    {
        public static AddBookCommand MapToAddBookCommand(this AddBookRequestDto dto)
        {
            return new AddBookCommand()
            {
                Author = dto.Author,
                ISBN = dto.ISBN,
                Title = dto.Title,
                YearOfPublication = dto.YearOfPublication
            };
        }
    }
}
