using Library.Application.ViewModel;
using Library.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Library.Application.Queries.GetLoans
{
    public class GetLoansQueryHandler : IRequestHandler<GetLoansQuery, List<LoanViewModel>>
    {
        private readonly ILogger<GetLoansQueryHandler> _logger;
        private readonly IUnitOfWork _uow;

        public GetLoansQueryHandler
        (
            ILogger<GetLoansQueryHandler> logger,
            IUnitOfWork uow
        )
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public async Task<List<LoanViewModel>> Handle(GetLoansQuery request, CancellationToken cancellationToken)
        {
            var watch = Stopwatch.StartNew();

            var loans = await _uow.LoanRepository.GetAsync();

            var loansViewModel = loans
                .Select(t => new LoanViewModel(t.Id, t.User.Name, t.Book.Title, t.LoanDate, t.DeadlineReturnDate, t.ReturnDate))
                .ToList();

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
