using Library.Application.ViewModel;
using Library.Core.Application;
using Library.Core.Notification;
using Library.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Library.Application.Queries.GetBooksById
{
    public class GetBooksByIdQueryHandler : BaseCommandHandler,
        IRequestHandler<GetBooksByIdQuery, BookViewModel>
    {
        private readonly ILogger<GetBooksByIdQueryHandler> _logger;
        private readonly IUnitOfWork _uow;

        public GetBooksByIdQueryHandler
        (
            ILogger<GetBooksByIdQueryHandler> logger,
            INotifier notifier,
            IUnitOfWork uow
        ) : base(notifier)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }
        public async Task<BookViewModel> Handle(GetBooksByIdQuery request, CancellationToken cancellationToken)
        {
            var watch = Stopwatch.StartNew();

            var book = await _uow.BookRepository.GetByIdAsync(request.Id);

            if (book == null)
                return null;

            var bookViewModel = new BookViewModel
                (
                    book.Id,
                    book.Title,
                    book.Author,
                    book.ISBN,
                    book.YearOfPublication,
                    book.Status,
                    book.IsBorrowed()
                );

            watch.Stop();

            _logger.LogDebug
            (
               $"{bookViewModel.Title} successfully " +
                $"ElapsedMilliseconds={watch.ElapsedMilliseconds}"
            );
            return bookViewModel;
        }
    }
}
