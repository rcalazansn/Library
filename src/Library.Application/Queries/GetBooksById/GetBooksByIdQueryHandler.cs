using Library.Application.Command.AddUser;
using Library.Application.ViewModel;
using Library.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Library.Application.Queries.GetBooksById
{
    public class GetBooksByIdQueryHandler : IRequestHandler<GetBooksByIdQuery, BookDetailsViewModel?>
    {
        private readonly ILogger<GetBooksByIdQueryHandler> _logger;
        private readonly IUnitOfWork _uow;

        public GetBooksByIdQueryHandler
        (
            ILogger<GetBooksByIdQueryHandler> logger,
            IUnitOfWork uow
        )
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }
        public async Task<BookDetailsViewModel?> Handle(GetBooksByIdQuery request, CancellationToken cancellationToken)
        {
            var watch = Stopwatch.StartNew();

            var book = await _uow.BookRepository.GetByIdAsync(request.Id);

            if (book == null) return null;

            var bookDetailViewModel = new BookDetailsViewModel
                (
                    book.Id,
                    book.Title,
                    book.Author,
                    book.ISBN,
                    book.YearOfPublication,
                    book.Status
                );

            watch.Stop();

            _logger.LogDebug
            (
               $"{bookDetailViewModel.Title} successfully " +
                $"ElapsedMilliseconds={watch.ElapsedMilliseconds}"
            );
            return bookDetailViewModel;
        }
    }
}
