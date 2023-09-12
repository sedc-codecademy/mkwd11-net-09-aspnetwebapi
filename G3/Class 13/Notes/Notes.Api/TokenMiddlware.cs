namespace Notes.Api
{
    public class TokenMiddlware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            string? token = context.Request.Headers.Authorization.FirstOrDefault();
            bool isValid = ValidateToken(token);
            if (isValid)
            {
                // fill in User property of HttpContext
                await next(context);
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            }
        }

        private bool ValidateToken(string? token)
        {
            return token == "This is token";
        }
    }
}
