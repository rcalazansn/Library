using Library.API.Dtos.Book;
using Library.API.Mappers.Book;
using Library.Application.Command.AddBook;
using Library.Application.Command.RemoveBook;
using Library.Application.Queries.GetBooks;
using Library.Application.Queries.GetBooksById;
using Library.Core.Notification;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Threading;

namespace Library.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : MainController
    {
        private readonly ILogger<BooksController> _logger;
        private readonly IMediator _mediator;

        public BooksController
        (
            ILogger<BooksController> logger,
            INotifier notifier,
            IMediator mediator
        ) : base(notifier)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

            _logger.LogTrace("BooksController has been initialized.");
        }

        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Request success!!")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation($"{DateTime.Now} - POST GetById route has been initialized. Ip: {GetUserIp()}");

            var query = new GetBooksByIdQuery(id);

            var book = await _mediator.Send(query);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, "Request success!!")]
        //[ProducesResponseType(typeof(AreasCoberturaAtivasResponseDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll([FromQuery] string query, CancellationToken cancellationToken)
        {

            /*
             * _logger.LogInformation($"GET ObterAreasCoberturaAtivas has been initialized. IP: {GetUserIp()}");

            var result = await _obterAreasCoberturaAtivasService.ProcessAsync(new AreasCoberturaAtivasQuery(),
                cancellationToken);

            return Ok(result.MapToAreasCoberturaAtivasResponseDto());
            */

            //_logger.LogInformation($"{DateTime.Now} - POST GetAll route has been initialized. Ip: {GetUserIp()}");

            //await _mediator.Send(dto.MapToAddBookCommand(), cancellationToken);

            //return CreatedAtAction(nameof(Created), dto);

            return Ok();
        }

        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, "Request success!!")]
        [ProducesResponseType(typeof(AddBookCommandResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadGateway)]
        public async Task<IActionResult> AddBook([FromBody] AddBookRequestDto dto, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{DateTime.Now} - POST AddBook route has been initialized. Ip: {GetUserIp()}");

            await _mediator.Send(dto.MapToAddBookCommand(), cancellationToken);

            return CreatedAtAction(nameof(Created), dto);
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
