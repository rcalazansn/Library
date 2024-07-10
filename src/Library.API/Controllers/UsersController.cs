using Library.API.Dtos.User;
using Library.API.Filters;
using Library.API.Mappers.User;
using Library.API.Middlewares;
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
        [ProducesResponseType(typeof(DefaultResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(DefaultErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ExceptionResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> AddUser([FromBody] AddUserRequestDto dto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"{DateTime.Now} - POST AddUser route has been initialized. Ip: {GetUserIp()}");

            var result = await _mediator.Send(dto.MapToAddUserCommand(), cancellationToken);

            return CustomResponse(HttpStatusCode.Created, result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(DefaultResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ExceptionResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll([FromQuery] string? query = null, int take = 5, int skip = 0, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"{DateTime.Now} - GET GetAll route has been initialized. Ip: {GetUserIp()}");

            var command = new GetUsersQuery(query, take, skip);

            var users = await _mediator.Send(command, cancellationToken);

            return CustomResponse(HttpStatusCode.OK, users);
        }
    }
}
