namespace api.Middlewares
{
    public class LoggingMiddleware(ILogger<LoggingMiddleware> _logger) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {

            _logger.LogInformation("Request: {Method} {Path}", context.Request.Method, context.Request.Path);
            // Log request information
            _logger.LogInformation("Incoming request: {Method} {Path}", context.Request.Method, context.Request.Path);

            // Call the next middleware in the pipeline
            await next(context);

            // Log response information
            _logger.LogInformation("Outgoing response: {StatusCode}", context.Response.StatusCode);
        }
    }
}