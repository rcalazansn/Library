using Library.Application.Command.AddBook;
using Library.Application.Command.RemoveBook;
using Library.Application.Queries.GetBooks;
using Library.Application.Queries.GetBooksById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> ObterPorId(int id)
        {
            var command = new GetBooksByIdQuery(id);

            var book = await _mediator.Send(command);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos([FromQuery] string? query = null)
        {
            var command = new GetBooksQuery(query);

            var books = await _mediator.Send(command);

            return Ok(books);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] AddBookCommand command)
        {
            await _mediator.Send(command);

            return CreatedAtAction(nameof(ObterPorId), command);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new RemoveBookCommand(id);

            await _mediator.Send(command);

            return NoContent();
        }
    }
}
