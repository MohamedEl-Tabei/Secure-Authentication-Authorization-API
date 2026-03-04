using Backend.Responses;
using System.ComponentModel.DataAnnotations;

namespace Backend.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                switch (ex)
                {
                    case ValidationException:
                        context.Response.StatusCode = 400;
                        break;
                    case UnauthorizedAccessException:
                        context.Response.StatusCode = 401;
                        break;
                    
                    default:
                        context.Response.StatusCode = 500;
                        break;
                }
                var errorResponse = new ErrorResponse() { Message = ex.Message };
                await context.Response.WriteAsJsonAsync(errorResponse);
            }
        }
    }
}
