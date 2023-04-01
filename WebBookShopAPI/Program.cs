using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using WebBookShopAPI.Data;
using WebBookShopAPI.Data.Errors;
using WebBookShopAPI.Data.Middleware;
using WebBookShopAPI.Data.Models.Identity;
using WebBookShopAPI.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DB services
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});
builder.Services.AddSingleton<IConnectionMultiplexer>(c =>
{
    var options = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"));
    return ConnectionMultiplexer.Connect(options);
});


// Identity Service
builder.Services.AddIdentityCore<AppUser>(opt =>
{
    // add identity options here
})
.AddEntityFrameworkStores<AppDbContext>()
.AddSignInManager<SignInManager<AppUser>>();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();


// Repository services
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();

// ShoppingCart services
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => {
    options.Cookie.Name = "Card";
    options.IdleTimeout = TimeSpan.FromMinutes(3600);
    options.Cookie.HttpOnly = false;
    options.Cookie.IsEssential = true; 
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

});

// Another services
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = actionContext =>
    {
        var errors = actionContext.ModelState
        .Where(e => e.Value.Errors.Count > 0)
        .SelectMany(x => x.Value.Errors)
        .Select(x => x.ErrorMessage).ToArray();

        var errorResponse = new ApiValidationErrorResponsecs
        {
            Errors = errors
        };

        return new BadRequestObjectResult(errorResponse);
    };
});

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("http://localhost:4200");
    });
});

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
app.UseSession();
app.UseCors("CorsPolicy");

// connection order is important, UseAuthentication always before UseAuthorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<AppDbContext>();
var userManager = services.GetRequiredService<UserManager<AppUser>>();
var logger = services.GetRequiredService<ILogger<Program>>();
try
{
    await context.Database.MigrateAsync();
    await AppDbContextSeed.SeedUsersAsync(userManager);
}
catch(Exception ex)
{
    logger.LogError(ex, "An error occured during migration");
}


app.Run();
