using Library.API.Dtos.Loan;
using Library.API.Filters;
using Library.API.Mappers.Loan;
using Library.API.Middlewares;
using Library.Application.Command.ReturnBook;
using Library.Application.Queries.GetLoans;
using Library.Core.Notification;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    public class LoansController: MainController
    {
        private readonly ILogger<LoansController> _logger;
        private readonly IMediator _mediator;

        public LoansController
        (
            ILogger<LoansController> logger,
            INotifier notifier,
            IMediator mediator
        ) : base(notifier)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

            _logger.LogTrace("BooksController has been initialized.");
        }

        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<DefaultResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ExceptionResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll(int take = 5, int skip = 0, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"{DateTime.Now} - GET GetAll route has been initialized. Ip: {GetUserIp()}");

            var command = new GetLoansQuery(take, skip);

            var result = await _mediator.Send(command, cancellationToken);

            return CustomResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(DefaultResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(DefaultErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ExceptionResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> AddLoan([FromBody] AddLoanRequestDto dto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"{DateTime.Now} - POST AddLoan route has been initialized. Ip: {GetUserIp()}");

            var result = await _mediator.Send(dto.MapToAddLoanCommand(), cancellationToken);

            return CustomResponse(HttpStatusCode.Created, result);
        }

        [HttpPut("{id}/return-book")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(DefaultErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ExceptionResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ReturnBook(int id, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"{DateTime.Now} - PUT ReturnBook route has been initialized. Ip: {GetUserIp()}");

            var command = new ReturnBookCommand (id);

            await _mediator.Send(command, cancellationToken);

            return CustomResponse(HttpStatusCode.NoContent);
        }
    }
}
