using Library.Application.Command.AddUser;
using Library.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Library.Application.Command.ReturnBook
{
    public class ReturnBookCommandHandler : IRequestHandler<ReturnBookCommand>
    {
        private readonly ILogger<AddUserCommandHandler> _logger;
        private readonly IUnitOfWork _uow;
        public ReturnBookCommandHandler
        (
            ILogger<AddUserCommandHandler> logger,
            IUnitOfWork uow
        )
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }
        public async Task Handle(ReturnBookCommand request, CancellationToken cancellationToken)
        {
            var watch = Stopwatch.StartNew();

            await _uow.LoanRepository.ReturnBookAsync(request.Id);

            var result = await _uow.CommitAsync(cancellationToken);

            watch.Stop();

            _logger.LogDebug
            (
                $"{request.Id} Return Book." +
                $"ElapsedMilliseconds={watch.ElapsedMilliseconds}"
            );
        }
    }
}
