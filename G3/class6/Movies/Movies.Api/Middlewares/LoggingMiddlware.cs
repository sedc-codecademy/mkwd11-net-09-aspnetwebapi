namespace Movies.Api.Middlewares
{
    public class LoggingMiddlware 
    {
        private readonly RequestDelegate next;
        private readonly ILoggerFactory loggerFactory;

        public LoggingMiddlware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            this.next = next;
            this.loggerFactory = loggerFactory;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var logger = loggerFactory.CreateLogger<LoggingMiddlware>();
            try
            {
                await next.Invoke(context);
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Error occured");
                throw;
            }
        }
    }
}
