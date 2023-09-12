using Profiles.BLL.Exceptions;

namespace Profiles.Api.Middleware
{
    public class ExceptionHandlingMiddlware
        : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (NotFoundException)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
            }
        }
    }

    public static class ExceptionHandlingMiddlwareConfig
    {
        public static IServiceCollection AddExceptionHandlingServices(this IServiceCollection services)
        {
            return services.AddTransient<ExceptionHandlingMiddlware>();
        }

        public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddlware>();
        }
    }
}
