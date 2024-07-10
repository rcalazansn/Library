using Library.API.Dtos.Book;
using Library.API.Filters;
using Library.API.Mappers.Book;
using Library.API.Middlewares;
using Library.Application.Command.RemoveBook;
using Library.Application.Queries.GetBooks;
using Library.Application.Queries.GetBooksById;
using Library.Core.Notification;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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
        [ProducesResponseType(typeof(DefaultResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(DefaultResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(DefaultErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ExceptionResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"{DateTime.Now} - GET GetById route has been initialized. Ip: {GetUserIp()}");

            var command = new GetBooksByIdQuery(id);

            var result = await _mediator.Send(command, cancellationToken);

            if (result == null)
                return CustomResponse(HttpStatusCode.NotFound);
            else
                return CustomResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<DefaultResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ExceptionResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll([FromQuery] string query = null, int take = 5, int skip = 0, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"{DateTime.Now} - GET GetAll route has been initialized. Ip: {GetUserIp()}");

            var command = new GetBooksQuery(query, take, skip);

            var result = await _mediator.Send(command, cancellationToken);

            return CustomResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(DefaultResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(DefaultErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ExceptionResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> AddBook([FromBody] AddBookRequestDto dto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"{DateTime.Now} - POST AddBook route has been initialized. Ip: {GetUserIp()}");

            var result = await _mediator.Send(dto.MapToAddBookCommand(), cancellationToken);

            return CustomResponse(HttpStatusCode.Created, result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(DefaultErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ExceptionResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"{DateTime.Now} Delete");

            var command = new RemoveBookCommand(id);

            await _mediator.Send(command, cancellationToken);

            return NoContent();
        }
    }
}
