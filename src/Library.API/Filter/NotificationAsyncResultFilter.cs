using Library.Core.Notification;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Library.API.Filters
{
    public class NotificationAsyncResultFilter : IAsyncResultFilter
    {
        private readonly ILogger<NotificationAsyncResultFilter> _logger;
        private readonly INotifier _notifier;

        public NotificationAsyncResultFilter
        (
            ILogger<NotificationAsyncResultFilter> logger,
            INotifier notifier
        )
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _notifier = notifier ?? throw new ArgumentNullException(nameof(notifier));
        }
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            context.HttpContext.Response.ContentType = "application/json";
            var result = context.Result as ObjectResult;

            if (_notifier.HasNotification())
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                var content = JsonSerializer.Serialize(GetDefaultErrorResponse());
                await context.HttpContext.Response.WriteAsync(content);

                return;
            }

            if (result?.Value != null && result?.StatusCode == 400)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                var badRequestResult = (ValidationProblemDetails)result.Value;

                _logger.LogWarning(GetLoggingBadRequest(badRequestResult));

                var content = JsonSerializer.Serialize(GetBadRequestResponse(badRequestResult));
                await context.HttpContext.Response.WriteAsync(content);

                return;
            }

            if (result?.Value != null && result?.StatusCode >= 200 && result?.StatusCode < 300)
            {
                context.HttpContext.Response.StatusCode = (int)result.StatusCode;
                var defaultResponse = new DefaultResponse(result.Value);

                await context.HttpContext.Response.WriteAsync(JsonSerializer.Serialize(defaultResponse));

                return;
            }

            await next();
        }
        private string GetLoggingBadRequest(ValidationProblemDetails problemDetails)
        {
            var logMessage = new StringBuilder();
            logMessage.Append($"A BadRequest occured: {problemDetails.Title}, Errors:");
            foreach (var error in problemDetails.Errors)
            {
                for (int i = 0; i < error.Value.Length; i++)
                {
                    logMessage.Append($"Key: {error.Key}, Value: {error.Value[i]}");
                }
            }

            return logMessage.ToString();
        }

        private DefaultErrorResponse GetDefaultErrorResponse()
        {
            var details = new List<DefaultErrorDetail>();
            foreach (var notification in _notifier.GetNotifications())
            {
                details.Add(new DefaultErrorDetail { Message = notification.Message });
            }

            return new DefaultErrorResponse(new DefaultError { Details = details });
        }

        private DefaultErrorResponse GetBadRequestResponse(ValidationProblemDetails problemDetails)
        {
            var details = new List<DefaultErrorDetail>();
            foreach (var error in problemDetails.Errors)
            {
                for (int i = 0; i < error.Value.Length; i++)
                {
                    details.Add(new DefaultErrorDetail { Message = $"Key: {error.Key}, Value: {error.Value[i]}" });
                }
            }

            return new DefaultErrorResponse(
                new DefaultError
                {
                    Details = details
                });
        }
    }

    public class DefaultResponse
    {
        public DefaultResponse(object payload) =>
            Payload = payload;

        public object Payload { get; }
        public bool Success => true;
    }
    public class DefaultErrorResponse
    {
        public DefaultErrorResponse(DefaultError error) =>
            Error = error;

        public object Payload => null;
        public bool Success => false;
        public DefaultError Error { get; }
    }
    public class DefaultError
    {
        public IReadOnlyCollection<DefaultErrorDetail> Details { get; set; }
    }
    public class DefaultErrorDetail
    {
        public string Message { get; set; }
    }
}
