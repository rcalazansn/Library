using Library.Application.ViewModel;
using MediatR;

namespace Library.Application.Queries.GetBooks
{
    public class GetBooksQuery : IRequest<List<BookViewModel>>
    {
        public GetBooksQuery(string query) => Query = query;

        public string Query { get; private set; }
    }
}
