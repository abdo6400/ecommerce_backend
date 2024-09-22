using api.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLocalizationConfiguration();
builder.Services.AddSwaggerConfiguration();
builder.Services.AddDatabaseConfiguration(builder.Configuration);
builder.Services.AddServiceConfiguration(builder.Configuration);
builder.Services.AddAuthenticationConfiguration(builder.Configuration);
builder.Services.AddMiddlewareConfiguration();

var app = builder.Build();

app.UseSwaggerConfiguration();
app.UseLocalizationConfiguration();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddlewareConfiguration();
app.MapControllers();
app.Run();
