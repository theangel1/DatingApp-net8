using API.Data;
using API.Extensions;
using API.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAplicattionServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);


var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod()
.WithOrigins("http://localhost:4200", "https://localhost:4200"));

//atento con el orden de este middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//aplicando migraciones y el seed. Es fuera del dependency injection, asi que... ojo
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
//try catch porq no estamos usando middleware exceptions
try
{
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedUsers(context);
}
catch (Exception ex)
{

    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "ocurrió un error durante la migracion");
}

app.Run();
