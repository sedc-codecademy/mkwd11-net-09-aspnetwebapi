namespace SecondApi
{
    public class Middleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            //do something to the request
            await next(context);
            //Do someting to the response 
        }
    }
}
