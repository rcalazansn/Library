using Library.Application.Command.AddLoan;
using Library.Application.Command.ReturnBook;
using Library.Application.Queries.GetLoans;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoansController: ControllerBase
    {
        private readonly ILogger<LoansController> _logger;
        private readonly IMediator _mediator;

        public LoansController(ILogger<LoansController> logger, IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("ObterTodos")]
        public async Task<IActionResult> ObterTodos()
        {
            var command = new GetLoansQuery();

            var loans = await _mediator.Send(command);

            return Ok(loans);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] AddLoanCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpPut("{id}/devolver")]
        public async Task<IActionResult> Devolver(int id)
        {
            var command = new ReturnBookCommand(id);

            await _mediator.Send(command);

            return Ok();
        }
    }
}
