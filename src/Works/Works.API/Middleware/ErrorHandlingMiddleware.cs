using Domain.Exceptions;
using System.Net;

namespace Works.API.Middleware
{
    public class ErrorHandlingMiddleware(RequestDelegate next)
    {
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        // DEVELOPMENT ONLY 
        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode statusCode = exception switch
            {
                NotFoundException =>
                    HttpStatusCode.NotFound,
            
                ValidationException =>
                    HttpStatusCode.BadRequest,
            
                ConflictException =>
                    HttpStatusCode.Conflict,
            
                ForbiddenException =>
                    HttpStatusCode.Forbidden,
            
                UnauthorizedException =>
                    HttpStatusCode.Unauthorized,
            
                _ =>
                    HttpStatusCode.InternalServerError
            };
            
            
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "text/plain; charset=utf-8";
            await context.Response.WriteAsync(exception.ToString());
        }
    }
}