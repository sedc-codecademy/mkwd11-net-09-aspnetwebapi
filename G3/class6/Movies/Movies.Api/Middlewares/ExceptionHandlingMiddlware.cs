using Movies.BLL.Exceptions;

namespace Movies.Api.Middlewares
{
    public class ExceptionHandlingMiddlware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
               await next.Invoke(context);
            }
            catch(NotFoundException nfe)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsync(nfe.Message);
            }
            catch(ValidationException ve)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync(ve.Message);
            }
        }
    }
}
