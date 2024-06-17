using Library.Application.Command.AddBook;
using Library.Application.Command.RemoveBook;
using Library.Application.Queries.GetBooks;
using Library.Application.Queries.GetBooksById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Library.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly ILogger<BooksController> _logger;
        private readonly IMediator _mediator;

        public BooksController(ILogger<BooksController> logger, IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Request success!!")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation($"{DateTime.Now} GetById");

            var command = new GetBooksByIdQuery(id);

            var book = await _mediator.Send(command);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, "Request success!!")]
        public async Task<IActionResult> GetAll([FromQuery] string? query = null)
        {
            _logger.LogInformation($"{DateTime.Now} GetAll");

            var command = new GetBooksQuery(query);

            var books = await _mediator.Send(command);

            return Ok(books);
        }

        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, "Request success!!")]
        public async Task<IActionResult> Created([FromBody] AddBookCommand command)
        {
            _logger.LogInformation($"{DateTime.Now} Created");

            await _mediator.Send(command);

            return CreatedAtAction(nameof(Created), command);
        }

        [HttpDelete("{id}")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Request success!!")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation($"{DateTime.Now} Delete");

            var command = new RemoveBookCommand(id);

            await _mediator.Send(command);

            return NoContent();
        }
    }
}
