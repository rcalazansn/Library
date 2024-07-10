using Library.Application.ViewModel;
using Library.Core.Application;
using Library.Core.Notification;
using Library.Domain.Models;
using Library.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Library.Application.Queries.GetUser
{
    public class GetUsersQueryHandler : BaseCommandHandler,
        IRequestHandler<GetUsersQuery, IReadOnlyCollection<UserViewModel>>
    {
        private readonly ILogger<GetUsersQueryHandler> _logger;
        private readonly IUnitOfWork _uow;
        public GetUsersQueryHandler
        (
            ILogger<GetUsersQueryHandler> logger,
            INotifier notifier,
            IUnitOfWork uow
        ) : base(notifier)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }
        public async Task<IReadOnlyCollection<UserViewModel>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var watch = Stopwatch.StartNew();

            IReadOnlyCollection<User> users = null;

            if (!string.IsNullOrWhiteSpace(request.Query))
            {
                users = await _uow.UserRepository
                    .GetDataAsync(f => (f.Email.ToUpper().Contains(request.Query.ToUpper()) || f.Name.ToUpper().Contains(request.Query.ToLower())),
                        take: request.Take, skip: request.Skip);
            }
            else
            {
                users = await _uow.UserRepository
                    .GetDataAsync(take: request.Take, skip: request.Skip);
            }

            var usersViewModel = users.Select(t => new UserViewModel(t.Id, t.Name, t.Email, t.UserTypeEnum)).ToList();

            watch.Stop();

            _logger.LogDebug
            (
               $"Count: {usersViewModel.Count} successfully " +
                $"ElapsedMilliseconds={watch.ElapsedMilliseconds}"
            );

            return usersViewModel;
        }
    }
}
