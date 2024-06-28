using Library.Application.Command.AddUser;
using Library.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Library.Application.Command.RemoveBook
{
    public class RemoveBookCommandHandler : IRequestHandler<RemoveBookCommand>
    {
        private readonly ILogger<AddUserCommandHandler> _logger;
        private readonly IUnitOfWork _uow;

        public RemoveBookCommandHandler
        (
            ILogger<AddUserCommandHandler> logger,
            IUnitOfWork uow
        )
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }
        public async Task Handle(RemoveBookCommand request, CancellationToken cancellationToken)
        {
            var watch = Stopwatch.StartNew();

            await _uow.BookRepository.RemoveAsync(request.Id);

            var result = await _uow.CommitAsync(cancellationToken);

            watch.Stop();

            _logger.LogDebug
            (
                $"{request.Id} removed record." +
                $"ElapsedMilliseconds={watch.ElapsedMilliseconds}"
            );
        }
    }
}
