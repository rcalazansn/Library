using Library.Application.ViewModel;
using MediatR;

namespace Library.Application.Queries.GetBooks
{
    public class GetBooksQuery : IRequest<IReadOnlyCollection<BookViewModel>>
    {
        public GetBooksQuery(string query, int take, int skip)
        {
            Query = query;
            Take = take;
            Skip = skip;
        }

        public string Query { get; private set; }
        public int Take { get; private set; } = 5;
        public int Skip { get; private set; } = 0;


    }
}
