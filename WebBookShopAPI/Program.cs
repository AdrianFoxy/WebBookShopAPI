using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebBookShopAPI.Data;
using WebBookShopAPI.Data.Middleware;
using WebBookShopAPI.Data.Models.Identity;
using WebBookShopAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddSwaggerDocumentation();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();
app.UseStatusCodePagesWithReExecute("/errors/{0}");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseStaticFiles(); // With this I can use wwwroot
app.UseCors("CorsPolicy");

// connection order is important, UseAuthentication always before UseAuthorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


// Seed db
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<AppDbContext>();
var userManager = services.GetRequiredService<UserManager<AppUser>>();
var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
var logger = services.GetRequiredService<ILogger<Program>>();
try
{
    await context.Database.MigrateAsync();
    await AppDbContextSeed.SeedUsersAsync(userManager, roleManager);
}
catch(Exception ex)
{
    logger.LogError(ex, "An error occured during migration");
}


app.Run();
