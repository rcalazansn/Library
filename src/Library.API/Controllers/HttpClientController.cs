using Library.API.Filters;
using Library.API.Middlewares;
using Library.Application.Queries.ViaCep;
using Library.Core.Notification;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    public class HttpClientController : MainController
    {
        private readonly ILogger<HttpClientController> _logger;
        private readonly IMediator _mediator;

        public HttpClientController
        (
            ILogger<HttpClientController> logger,
            INotifier notifier,
            IMediator mediator
        ) : base(notifier)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

            _logger.LogTrace("HttpClientController has been initialized.");
        }

        [HttpGet("{cep}/refit")]
        [ProducesResponseType(typeof(DefaultResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(DefaultResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(DefaultErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ExceptionResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ViaCepRefit(string cep, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"{DateTime.Now} - GET ViaCepRefit route has been initialized. Ip: {GetUserIp()}");

            var command = new GetViaCepByCepQuery(cep);

            var result = await _mediator.Send(command, cancellationToken);

            if (result == null)
                return CustomResponse(HttpStatusCode.NotFound);
            else
                return CustomResponse(HttpStatusCode.OK, result);
        }

        [HttpGet("{cep}/client-factory")]
        [ProducesResponseType(typeof(DefaultResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(DefaultResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(DefaultErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ExceptionResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ViaCepClientFactory(string cep, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"{DateTime.Now} - GET ViaCepClientFactory route has been initialized. Ip: {GetUserIp()}");

            var command = new GetViaCepByCepClientFactoryQuery(cep);

            var result = await _mediator.Send(command, cancellationToken);

            if (result == null)
                return CustomResponse(HttpStatusCode.NotFound);
            else
                return CustomResponse(HttpStatusCode.OK, result);
        }
    }
}
