using Library.Application.Notifications.Loan;
using Library.Core.Application;
using Library.Core.Notification;
using Library.Domain.Models;
using Library.Domain.Validations;
using Library.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Library.Application.Command.AddLoan
{
    public class AddLoanCommandHandler : BaseHandler,
        IRequestHandler<AddLoanCommand, AddLoanCommandResponse>
    {
        private readonly ILogger<AddLoanCommandHandler> _logger;
        private readonly IUnitOfWork _uow;
        private readonly IMediator _mediator;
        public AddLoanCommandHandler
        (
            ILogger<AddLoanCommandHandler> logger,
            INotifier notifier,
            IMediator mediator,
            IUnitOfWork uow
        ) : base(notifier)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        public async Task<AddLoanCommandResponse> Handle(AddLoanCommand request, CancellationToken cancellationToken)
        {
            var watch = Stopwatch.StartNew();

            var userDb = await _uow.UserRepository.GetByIdAsync(request.UserId);
            if (userDb == null)
            {
                Notify("User not found");
                return null;
            }

            var bookDb = await _uow.BookRepository.GetByIdAsync(request.BookId);
            if (bookDb == null)
            {
                Notify("Book not found");
                return null;
            }

            if (!bookDb.IsAvailable())
            {
                Notify("The book is not available for loan");
                return null;
            }

            if (bookDb.IsBorrowed())
            {
                Notify("book is on loan");
                return null;
            }

            var loan = new Loan(request.UserId, request.BookId);

            if (!ExecuteValidation(new AddLoanValidation(), loan))
                return null;

            var loanDb = await _uow.LoanRepository.GetDataAsync(_ =>
                _.UserId == request.UserId &&
                _.Book.Status == Domain.Enum.BookStatusEnum.Borrowed);

            if (loanDb.Count > 3)
            {
                Notify("User already has maximum amount of loans");
                return null;
            }

            bookDb.Borrowed();

            _uow.LoanRepository.Add(loan);
            _uow.BookRepository.Update(bookDb);

            var success = await _uow.SaveAsync(cancellationToken);

            if (!success)
                return null;

            //publish (rabbitMQ)
            await _mediator.Publish(new LoanCreatedNotification(loan));

            watch.Stop();

            _logger.LogDebug
            (
                $"{loan.Id} saved record." +
                $"ElapsedMilliseconds={watch.ElapsedMilliseconds}"
            );

            return new AddLoanCommandResponse()
            {
                Id = loan.Id
            };
        }
    }
}
