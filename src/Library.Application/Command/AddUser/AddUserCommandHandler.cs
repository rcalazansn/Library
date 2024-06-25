using Library.Domain.Models;
using Library.Domain.Validations;
using Library.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Library.Application.Command.AddUser
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand>
    {
        private readonly ILogger<AddUserCommandHandler> _logger;
        private readonly IUnitOfWork _uow;

        public AddUserCommandHandler
        (
            ILogger<AddUserCommandHandler> logger,
            IUnitOfWork uow
        )
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }
        public async Task Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var watch = Stopwatch.StartNew();

            var user = new User(request.Name, request.Email, request.UserTypeEnm);
            
            await _uow.UserRepository.AddAsync(user);

            var validator = new AddUserValidation().Validate(user);

            if (validator.IsValid)
            {
                var result = _uow.Commit(cancellationToken);
            }
            else
            {
                foreach (var error in validator.Errors)
                {
                    _logger.LogWarning($"{error.ErrorMessage}");
                }
            }


            watch.Stop();

            _logger.LogDebug
            (
                $"{request.Name} saved record." +
                $"ElapsedMilliseconds={watch.ElapsedMilliseconds}"
            );
        }
    }
}
