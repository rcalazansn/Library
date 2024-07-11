using Library.Application.ViewModel;
using Library.Core.Application;
using Library.Core.Notification;
using Library.Domain.Models;
using Library.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Library.Application.Queries.GetBooks
{
    public class GetBooksQueryHandler : BaseHandler,
        IRequestHandler<GetBooksQuery, IReadOnlyCollection<BookViewModel>>
    {
        private readonly ILogger<GetBooksQueryHandler> _logger;
        private readonly IUnitOfWork _uow;
        public GetBooksQueryHandler
        (
            ILogger<GetBooksQueryHandler> logger,
            INotifier notifier,
            IUnitOfWork uow
        ) : base(notifier)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }
        public async Task<IReadOnlyCollection<BookViewModel>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            var watch = Stopwatch.StartNew();

            IReadOnlyCollection<Book> books = null;

            if (!string.IsNullOrWhiteSpace(request.Query))
            {
                books = await _uow.BookRepository
                    .GetDataAsync(f => (f.Author.ToUpper().Contains(request.Query.ToUpper()) || f.Title.ToUpper().Contains(request.Query.ToLower())),
                        take: request.Take, skip: request.Skip);
            }
            else
            {
                books = await _uow.BookRepository
                    .GetDataAsync(take: request.Take, skip: request.Skip);
            }

            var booksViewModel = books.Select(_ => 
                new BookViewModel(_.Id, _.Title, _.Author, _.ISBN, _.YearOfPublication, _.Status, _.IsBorrowed())).ToList();

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
