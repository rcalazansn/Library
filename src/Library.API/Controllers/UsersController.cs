using Library.Application.Command.AddUser;
using Library.Application.Queries.GetUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Cadastrar([FromBody] AddUserCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos([FromQuery] string? query = null)
        {
            var command = new GetUsersQuery(query);

            var users = await _mediator.Send(command);

            return Ok(users);
        }
    }
}
