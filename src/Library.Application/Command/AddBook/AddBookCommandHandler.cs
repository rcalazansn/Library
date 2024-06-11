using Library.Application.Command.AddUser;
using Library.Domain.Models;
using Library.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Library.Application.Command.AddBook
{
    public class AddBookCommandHandler : IRequestHandler<AddBookCommand>
    {
        private readonly ILogger<AddBookCommandHandler> _logger;
        private readonly IUnitOfWork _uow;

        public AddBookCommandHandler
        (
            ILogger<AddBookCommandHandler> logger,
            IUnitOfWork uow
        )
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }
        public async Task Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            var watch = Stopwatch.StartNew();

            var book = new Book(request.Title, request.Author, request.ISBN, request.YearOfPublication);

            await _uow.BookRepository.AddAsync(book);

            var result = _uow.Commit(cancellationToken);

            watch.Stop();

            _logger.LogDebug
            (
                $"{request.Title} saved record." +
                $"ElapsedMilliseconds={watch.ElapsedMilliseconds}"
            );
        }
    }
}
