using Library.Core.Application;
using Library.Core.Notification;
using Library.Domain.Models;
using Library.Domain.Validations;
using Library.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Library.Application.Command.AddUser
{
    public class AddUserCommandHandler : BaseCommandHandler,
        IRequestHandler<AddUserCommand, AddUserCommandResponse>
    {
        private readonly ILogger<AddUserCommandHandler> _logger;
        private readonly IUnitOfWork _uow;

        public AddUserCommandHandler
        (
            ILogger<AddUserCommandHandler> logger,
            INotifier notifier,
            IUnitOfWork uow
        ) : base(notifier)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }
        public async Task<AddUserCommandResponse> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var watch = Stopwatch.StartNew();

            var user = new User(request.Name, request.Email, request.UserTypeEnm);

            if (!ExecuteValidation(new AddUserValidation(), user))
                return null;

            _uow.UserRepository.Add(user);

            var success = await _uow.CommitAsync(cancellationToken);

            if (!success)
                return null;

            //publish (rabbitMQ)

            watch.Stop();

            _logger.LogDebug
            (
                $"{request.Name} saved record." +
                $"ElapsedMilliseconds={watch.ElapsedMilliseconds}"
            );

            return new AddUserCommandResponse()
            {
                Id = user.Id,
                Email = request.Email,
                Name = request.Name,
                UserTypeEnm = request.UserTypeEnm
            }; ;
        }
    }
}
