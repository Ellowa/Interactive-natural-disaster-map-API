using InteractiveNaturalDisasterMap.Application.Exceptions;
using System.Net;

namespace InteractiveNaturalDisasterMap.Web.Middlewares.ExceptionHandling
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _requestNext;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate requestNext, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _requestNext = requestNext;
            _logger = logger;
        }

        private async Task HandleAsync(HttpContext context, HttpStatusCode statusCode, string exMessage)
        {
            _logger.LogError(exMessage);

            HttpResponse response = context.Response;

            response.ContentType = "application/json";
            response.StatusCode = (int)statusCode;

            ErrorMessage errorMessage = new()
            {
                Message = exMessage,
                StatusCode = (int)statusCode
            };

            await response.WriteAsJsonAsync(errorMessage);
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _requestNext(context);
            }
            catch (AuthorizationException ex)
            {
                await HandleAsync(context, HttpStatusCode.Forbidden, ex.Message);
            }
            catch (RequestArgumentException ex)
            {
                await HandleAsync(context, HttpStatusCode.BadRequest, ex.Message);
            }
            catch (NotFoundException ex)
            {
                await HandleAsync(context, HttpStatusCode.NotFound, ex.Message);
            }
            catch (Exception ex)
            {
                await HandleAsync(context, HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
