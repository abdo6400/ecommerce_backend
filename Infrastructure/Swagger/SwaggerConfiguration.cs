using api.Swagger;
using Microsoft.OpenApi.Models;

namespace api.Infrastructure
{
    public static class SwaggerConfiguration
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                // Admin
                options.SwaggerDoc("v1-admin", new OpenApiInfo { Title = "Admin API", Version = "v1" });

                // Customer
                options.SwaggerDoc("v1-customer", new OpenApiInfo { Title = "Customer API", Version = "v1" });

                // Delivery
                options.SwaggerDoc("v1-delivery", new OpenApiInfo { Title = "Delivery API", Version = "v1" });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
                });

                options.OperationFilter<AddAcceptLanguageHeader>();
            });
        }

        public static void UseSwaggerConfiguration(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1-admin/swagger.json", "Admin API v1");
                c.SwaggerEndpoint("/swagger/v1-customer/swagger.json", "Customer API v1");
                c.SwaggerEndpoint("/swagger/v1-delivery/swagger.json", "Delivery API v1");
            });
        }
    }
}