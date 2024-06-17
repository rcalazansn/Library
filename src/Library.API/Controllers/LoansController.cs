using Library.Application.Command.AddLoan;
using Library.Application.Command.ReturnBook;
using Library.Application.Queries.GetLoans;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

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

        [HttpGet("get-all")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Request success!!")]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation($"{DateTime.Now} GetAll");

            var command = new GetLoansQuery();

            var loans = await _mediator.Send(command);

            return Ok(loans);
        }

        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, "Request success!!")]
        public async Task<IActionResult> Create([FromBody] AddLoanCommand command)
        {
            _logger.LogInformation($"{DateTime.Now} Create");

            await _mediator.Send(command);

            return NoContent();
        }

        [HttpPut("{id}/return-book")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Request success!!")]
        public async Task<IActionResult> ReturnBook(int id)
        {
            _logger.LogInformation($"{DateTime.Now} ReturnBook");

            var command = new ReturnBookCommand(id);

            await _mediator.Send(command);

            return Ok();
        }
    }
}
