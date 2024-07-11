using Library.Application.Notifications.Book;
using Library.Core.Application;
using Library.Core.Notification;
using Library.Domain.Models;
using Library.Domain.Validations;
using Library.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Library.Application.Command.AddBook
{
    public class AddBookCommandHandler : BaseHandler,
        IRequestHandler<AddBookCommand, AddBookCommandResponse>
    {
        private readonly ILogger<AddBookCommandHandler> _logger;
        private readonly IUnitOfWork _uow;
        private readonly IMediator _mediator;
        public AddBookCommandHandler
        (
            ILogger<AddBookCommandHandler> logger,
            INotifier notifier,
            IMediator mediator,
            IUnitOfWork uow
        ) : base(notifier)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<AddBookCommandResponse> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            var watch = Stopwatch.StartNew();

            var book = new Book(request.Title, request.Author, request.ISBN, request.YearOfPublication);

            if (!ExecuteValidation(new AddBookValidation(), book))
                return null;

            var bookDb = await _uow.BookRepository.FirstAsync(_ => _.Title.ToUpper().Equals(book.Title.ToUpper()) && _.Author.ToUpper().Equals(book.Author.ToUpper()));

            if (bookDb != null)
            {
                Notify("Title already registered to the Author");
                return null;
            }

            _uow.BookRepository.Add(book);

            var success = await _uow.CommitAsync(cancellationToken);

            if (!success)
                return null;

            //publish (rabbitMQ)
            await _mediator.Publish(new BookCreatedNotification(book));

            watch.Stop();

            _logger.LogDebug
            (
                $"{request.Title} saved record." +
                $"ElapsedMilliseconds={watch.ElapsedMilliseconds}"
            );

            return new AddBookCommandResponse()
            {
                Id = book.Id,
                Author = request.Author,
                YearOfPublication = request.YearOfPublication,
                Title = request.Title,
                ISBN = request.ISBN
            };
        }
    }
}
