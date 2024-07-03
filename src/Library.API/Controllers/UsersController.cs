using Library.API.Dtos.User;
using Library.API.Mappers.User;
using Library.Application.Command.AddUser;
using Library.Application.Queries.GetUser;
using Library.Core.Notification;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    
    public class UsersController : MainController
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IMediator _mediator;

        public UsersController
        (
            ILogger<UsersController> logger, 
            INotifier notifier, 
            IMediator mediator
        ) : base(notifier)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

            _logger.LogTrace("UsersController has been initialized.");
        }

        [HttpPost]
        [ProducesResponseType(typeof(AddUserCommandResponse), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> AddUser([FromBody] AddUserRequestDto dto, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{DateTime.Now} - POST AddUser route has been initialized. Ip: {GetUserIp()}");

            await _mediator.Send(dto.MapToAddUserCommand(), cancellationToken);

            return CustomResponse(HttpStatusCode.Created);
        }

        [HttpGet]
        [ProducesResponseType(typeof(AddUserRequestDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll([FromQuery] string? query = null)
        {
            _logger.LogInformation($"{DateTime.Now} GetAll");

            var command = new GetUsersQuery(query);

            var users = await _mediator.Send(command);

            return CustomResponse(HttpStatusCode.OK, users);
        }
    }
}
