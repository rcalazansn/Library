using Library.Application.Command.AddUser;
using Library.Domain.Models;
using Library.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Library.Application.Command.AddLoan
{
    public class AddLoanCommandHandler : IRequestHandler<AddLoanCommand>
    {
        private readonly ILogger<AddLoanCommandHandler> _logger;
        private readonly IUnitOfWork _uow;
        public AddLoanCommandHandler
        (
            ILogger<AddLoanCommandHandler> logger,
            IUnitOfWork uow
        )
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }
        public async Task Handle(AddLoanCommand request, CancellationToken cancellationToken)
        {
            var watch = Stopwatch.StartNew();

            var loan = new Loan(request.UserId, request.BookId, request.DeadlineReturnDate);

            await _uow.LoanRepository.AddAsync(loan);

            var result = _uow.Commit(cancellationToken);

            watch.Stop();

            _logger.LogDebug
            (
                $"{request.UserId} saved record." +
                $"ElapsedMilliseconds={watch.ElapsedMilliseconds}"
            );
        }
    }
}
