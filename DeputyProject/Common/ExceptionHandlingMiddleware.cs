using System.Net;
using System.Text.Json;

namespace DeputyProject.Common
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Unhandled exception occurred.");

                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(
            HttpContext context,
            Exception exception)
        {
            context.Response.ContentType = "application/json";

            // Bad input (e.g. a rejected file type/size) is a client error,
            // not a server fault — everything else stays a generic 500 so we
            // never leak internal exception details to the client.
            var isClientError = exception is ArgumentException;

            context.Response.StatusCode = isClientError
                ? (int)HttpStatusCode.BadRequest
                : (int)HttpStatusCode.InternalServerError;

            var response = new
            {
                isSuccess = false,
                status = context.Response.StatusCode,
                error = isClientError
                    ? exception.Message
                    : "An unexpected error occurred.",
                value = (object?)null,
                message = isClientError
                    ? exception.Message
                    : "حدث خطأ غير متوقع."
            };

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }
    }
}
