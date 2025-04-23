using Library.Application.Command.AddUser;
using Library.Application.Notifications.Loan;
using Library.Core.Application;
using Library.Core.Notification;
using Library.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Library.Application.Command.ReturnBook
{
    public class ReturnBookCommandHandler : BaseHandler,
        IRequestHandler<ReturnBookCommand>
    {
        private readonly ILogger<AddUserCommandHandler> _logger;
        private readonly IUnitOfWork _uow;
        private readonly IMediator _mediator;
        public ReturnBookCommandHandler
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
        public async Task Handle(ReturnBookCommand request, CancellationToken cancellationToken)
        {
            var watch = Stopwatch.StartNew();

            var loan = await _uow.LoanRepository.GetByIdAsync(_=>_.Id.Equals(request.Id),
                include: _ => _.Include(b => b.Book).Include(u => u.User));

            if (loan == null)
            {
                Notify("not found");
                return;
            }

            if (!loan.Book.IsBorrowed())
            {
                Notify("the book is not on loan");
                return;
            }

            await _uow.LoanRepository.ReturnBookAsync(request.Id);

            var success = await _uow.SaveAsync(cancellationToken);

            if (!success)
                return;

            //publish (rabbitMQ)
            await _mediator.Publish(new ReturnBookNotification());

            watch.Stop();

            _logger.LogDebug
            (
                $"{request.Id} saved record." +
                $"ElapsedMilliseconds={watch.ElapsedMilliseconds}"
            );
        }
    }
}
