using Library.Application.Command.AddUser;
using Library.Application.ViewModel;
using Library.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Library.Application.Queries.GetUser
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<UserViewModel>>
    {
        private readonly ILogger<GetUsersQueryHandler> _logger;
        private readonly IUnitOfWork _uow;

        public GetUsersQueryHandler
        (
            ILogger<GetUsersQueryHandler> logger,
            IUnitOfWork uow
        )
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }
        public async Task<List<UserViewModel>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var watch = Stopwatch.StartNew();

            var users = await _uow.UserRepository.GetAsync(request.Query);

            if (!string.IsNullOrWhiteSpace(request.Query))
            {
                //TODO:
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
