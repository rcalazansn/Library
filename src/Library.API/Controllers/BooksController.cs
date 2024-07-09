using Library.API.Dtos.Book;
using Library.API.Dtos.User;
using Library.API.Mappers.Book;
using Library.Application.Command.AddBook;
using Library.Application.Command.RemoveBook;
using Library.Application.Queries.GetBooks;
using Library.Application.Queries.GetBooksById;
using Library.Application.Queries.GetUser;
using Library.Application.ViewModel;
using Library.Core.Notification;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
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
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"{DateTime.Now} - POST GetById route has been initialized. Ip: {GetUserIp()}");

            var query = new GetBooksByIdQuery(id);

            var book = await _mediator.Send(query, cancellationToken);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<BookViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll([FromQuery] string query = null, int take = 5, int skip = 0, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"{DateTime.Now} - GET GetAll route has been initialized. Ip: {GetUserIp()}");

            var command = new GetBooksQuery(query, take, skip);

            var users = await _mediator.Send(command, cancellationToken);

            return CustomResponse(HttpStatusCode.OK, users);
        }

        [HttpPost]
        [ProducesResponseType(typeof(AddBookCommandResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadGateway)]
        public async Task<IActionResult> AddBook([FromBody] AddBookRequestDto dto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"{DateTime.Now} - POST AddBook route has been initialized. Ip: {GetUserIp()}");

            var result = await _mediator.Send(dto.MapToAddBookCommand(), cancellationToken);

            return CustomResponse(HttpStatusCode.Created, result);
        }

        [HttpDelete("{id}")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Request success!!")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"{DateTime.Now} Delete");

            var command = new RemoveBookCommand(id);

            await _mediator.Send(command, cancellationToken);

            return NoContent();
        }
    }
}
