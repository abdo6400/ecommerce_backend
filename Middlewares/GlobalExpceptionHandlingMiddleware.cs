using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace api.Middlewares
{
    public class GlobalExpceptionHandlingMiddleware(ILogger<GlobalExpceptionHandlingMiddleware> _logger) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                ProblemDetails problemDetails = new()
                {
                    Type = "Server Error",
                    Title = "Server Error",
                    Status = (int)HttpStatusCode.InternalServerError,
                    Detail = "Internal Server Error",
                };
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                string json = JsonSerializer.Serialize(problemDetails);
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(json);
            }
        }
    }
}