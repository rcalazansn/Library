using Library.Application.Command.AddUser;
using Library.Application.Queries.GetUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController: ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IMediator _mediator;

        public UsersController(ILogger<UsersController> logger, IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, "Request success!!")]
        public async Task<IActionResult> Create([FromBody] AddUserCommand command)
        {
            _logger.LogInformation($"{DateTime.Now} GetAll");

            await _mediator.Send(command);

            return NoContent();
        }

        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, "Request success!!")]
        public async Task<IActionResult> GetAll([FromQuery] string? query = null)
        {
            _logger.LogInformation($"{DateTime.Now} GetAll");

            var command = new GetUsersQuery(query);

            var users = await _mediator.Send(command);

            return Ok(users);
        }
    }
}
