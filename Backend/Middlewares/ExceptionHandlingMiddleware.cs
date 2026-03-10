using Backend.Responses;
using BL;
using BL.Exceptions;
using FluentValidation;

namespace Backend.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (AppBaseException ex)
            {
                var errorResponse = new ErrorResponse()
                {
                    Errors = ex.Errors ?? new List<Error>(){
                        new Error()
                        {
                            Messages = new List<string>() { ex.Message },
                            PropertyName = null
                        } 
                    }
                };
                switch (ex)
                {
                    case AppValidationException:
                        context.Response.StatusCode = 400;
                        break;
                    case AppNotFoundException:
                        context.Response.StatusCode = 404;
                        break;
                    case AppTooManyRequestsException:
                        context.Response.StatusCode = 429;
                        break;
                    default:
                        context.Response.StatusCode = 404;
                        break;
                }
                await context.Response.WriteAsJsonAsync(errorResponse);
            }
            catch (Exception ex)
            {
                var errorResponse = new ErrorResponse();
                context.Response.StatusCode = 500;
                errorResponse.Errors = new List<Error>(){new Error()
                {
                    Messages = new List<string>() { "An unexpected error occurred. Please try again later." },
                    PropertyName = null
                } };
                _logger.LogError(
                    $"___________________________________________________________\n" +
                    $"Exception: {ex.GetType().Name}, Message: {ex.Message}, Source: {ex.Source}" +
                    $"\n___________________________________________________________");
                await context.Response.WriteAsJsonAsync(errorResponse);

            }
        }
    }
}
