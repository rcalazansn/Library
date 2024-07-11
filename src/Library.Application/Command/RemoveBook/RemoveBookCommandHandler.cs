using Library.Application.Command.AddUser;
using Library.Application.Notifications.Book;
using Library.Core.Application;
using Library.Core.Notification;
using Library.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Library.Application.Command.RemoveBook
{
    public class RemoveBookCommandHandler : BaseHandler,
        IRequestHandler<RemoveBookCommand>
    {
        private readonly ILogger<AddUserCommandHandler> _logger;
        private readonly IUnitOfWork _uow;
        private readonly IMediator _mediator;
        public RemoveBookCommandHandler
        (
            ILogger<AddUserCommandHandler> logger,
            INotifier notifier,
            IMediator mediator,
            IUnitOfWork uow
        ) : base(notifier)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        public async Task Handle(RemoveBookCommand request, CancellationToken cancellationToken)
        {
            var watch = Stopwatch.StartNew();

            var book = await _uow.BookRepository.GetByIdAsync(request.Id);

            if (book == null)
            {
                Notify($"There is no record with id {request.Id}");
                return;
            }

            _uow.BookRepository.Remove(book);

            var success = await _uow.CommitAsync(cancellationToken);

            if (!success)
                return;

            //publish (rabbitMQ)
            await _mediator.Publish(new BookRemovedNotification(book));

            watch.Stop();

            _logger.LogDebug
            (
                $"{book.Title}/{book.Author} removed record." +
                $"ElapsedMilliseconds={watch.ElapsedMilliseconds}"
            );
        }
    }
}
