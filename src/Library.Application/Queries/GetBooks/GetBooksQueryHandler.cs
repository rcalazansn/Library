using Library.Application.Command.AddUser;
using Library.Application.ViewModel;
using Library.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Library.Application.Queries.GetBooks
{
    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, List<BookViewModel>>
    {
        private readonly ILogger<GetBooksQueryHandler> _logger;
        private readonly IUnitOfWork _uow;
        public GetBooksQueryHandler
        (
            ILogger<GetBooksQueryHandler> logger,
            IUnitOfWork uow
        )
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }
        public async Task<List<BookViewModel>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            var watch = Stopwatch.StartNew();

            var books = await _uow.BookRepository.GetAsync(request.Query);

            var booksViewModel = books
                .Select(t => new BookViewModel(t.Id, t.Title, t.Author, t.ISBN, t.YearOfPublication, t.Status, t.Loans.Any()))
                .ToList();

            watch.Stop();

            _logger.LogDebug
            (
               $"Count: {booksViewModel.Count} successfully " +
                $"ElapsedMilliseconds={watch.ElapsedMilliseconds}"
            );

            return booksViewModel;
        }
    }
}
