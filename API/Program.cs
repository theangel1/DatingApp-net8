using API.Data;
using API.Entities;
using API.Extensions;
using API.Middleware;
using API.SignalR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAplicattionServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);


var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowCredentials()
.WithOrigins("http://localhost:4200", "https://localhost:4200"));

//atento con el orden de este middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<PresenceHub>("hubs/presence");
app.MapHub<MessageHub>("hubs/message");

//aplicando migraciones y el seed. Es fuera del dependency injection, asi que... ojo
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
//try catch porq no estamos usando middleware exceptions
try
{
    var context = services.GetRequiredService<DataContext>();
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
    await context.Database.MigrateAsync();
//lo siguiente para remover comportamientos raros al momento de que se nos cierre la app. clase 437 del curso....
    await context.Database.ExecuteSqlRawAsync("DELETE FROM [Connections]")
    await Seed.SeedUsers(userManager, roleManager);
}
catch (Exception ex)
{

    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "ocurrió un error durante la migracion");
}

app.Run();
