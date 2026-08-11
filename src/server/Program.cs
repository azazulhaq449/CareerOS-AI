using CareerOS.Server.Data;
using CareerOS.Server.Data.Entities;
using CareerOS.Server.Data.Seed;
using CareerOS.Server.Extensions;
using CareerOS.Server.Services.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options => options.Filters.Add<NotFoundExceptionFilter>());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCareerOSDatabase(builder.Configuration);
builder.Services.AddPortfolioFeature();
builder.Services.AddCareerOSAuthentication(builder.Configuration);
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

const string ClientDevCorsPolicy = "ClientDevCorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(ClientDevCorsPolicy, policy =>
    {
        policy.WithOrigins("http://localhost:5173", "http://localhost:5174")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();

// Apply pending migrations and seed initial data. Auto-migrating on startup
// is a deliberate simplicity choice for this project's current stage
// (single deploy target, no team coordinating migrations yet).
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CareerOSDbContext>();
    await db.Database.MigrateAsync();
    await PortfolioDataSeeder.SeedAsync(db);

    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    await IdentityDataSeeder.SeedAsync(userManager, app.Configuration);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(ClientDevCorsPolicy);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
