using Library.API.Filters;
using Newtonsoft.Json;
using System.Net;

namespace Library.API.Middlewares
{
    public class ErrorHandlerMiddleware : IMiddleware
    {
        private readonly ILoggerFactory loggerFactory;

        public ErrorHandlerMiddleware(ILoggerFactory loggerFactory)
        {
            this.loggerFactory = loggerFactory;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception, loggerFactory);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception, ILoggerFactory loggerFactory)
        {
            var exceptionObject = new ExceptionResponse(exception);
            var exceptionSerialized = System.Text.Json.JsonSerializer.Serialize(exceptionObject);

            if (!context.Response.HasStarted)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                await context.Response.WriteAsync(exceptionSerialized);
            }
        }
    }
}
