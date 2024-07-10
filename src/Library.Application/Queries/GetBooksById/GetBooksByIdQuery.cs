using Library.Application.ViewModel;
using MediatR;

namespace Library.Application.Queries.GetBooksById
{
    public class GetBooksByIdQuery : IRequest<BookViewModel>
    {
        public GetBooksByIdQuery(int id) => Id = id;

        public int Id { get; private set; }
    }
}
