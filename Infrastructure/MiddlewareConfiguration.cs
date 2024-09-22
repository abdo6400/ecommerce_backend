using api.Middlewares;

namespace api.Infrastructure
{
    public static class MiddlewareConfiguration
    {
        public static void AddMiddlewareConfiguration(this IServiceCollection services)
        {
            services.AddSingleton<LoggingMiddleware>();
            services.AddExceptionHandler<ValidationExceptionHandler>();
            services.AddExceptionHandler<GlobalExceptionHandler>();
        }

        public static void UseMiddlewareConfiguration(this WebApplication app)
        {
            app.UseMiddleware<LoggingMiddleware>();
        }
    }
}