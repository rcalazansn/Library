using Library.Application.ViewModel;
using Library.Core.Application;
using Library.Core.Notification;
using Library.Domain.Models;
using Library.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Library.Application.Queries.GetLoans
{
    public class GetLoansQueryHandler : BaseHandler,
        IRequestHandler<GetLoansQuery, IReadOnlyCollection<LoanViewModel>>
    {
        private readonly ILogger<GetLoansQueryHandler> _logger;
        private readonly IUnitOfWork _uow;

        public GetLoansQueryHandler
         (
            ILogger<GetLoansQueryHandler> logger,
            INotifier notifier,
            IUnitOfWork uow
        ) : base(notifier)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public async Task<IReadOnlyCollection<LoanViewModel>> Handle(GetLoansQuery request, CancellationToken cancellationToken)
        {
            var watch = Stopwatch.StartNew();

            IReadOnlyCollection<Loan> loans = await _uow.LoanRepository.GetDataAsync(
                include: _ => _.Include(b => b.Book).Include(u=>u.User),
                take: request.Take,
                skip: request.Skip);

            var loansViewModel = loans.Select(_ =>
                new LoanViewModel(_.Id, _.UserId, _.BookId, _.User.Name, _.Book.Title, _.LoanDate, 
                    _.DeadlineReturnDate, _.ReturnDate, _.Book.Status)).ToList();

            watch.Stop();

            _logger.LogDebug
            (
               $"Count: {loansViewModel.Count} successfully " +
                $"ElapsedMilliseconds={watch.ElapsedMilliseconds}"
            );

            return loansViewModel;
        }
    }
}
