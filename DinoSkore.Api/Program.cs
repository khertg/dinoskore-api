using DinoSkore.Api.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Read connection string from environment if on Railway, else use appsettings
var databaseUrl = Environment.GetEnvironmentVariable("ConnectionStrings__Database")
                  ?? builder.Configuration.GetConnectionString("Database");

// Convert URL to Npgsql format and add SSL
var npgsqlBuilder = new NpgsqlConnectionStringBuilder(databaseUrl)
{
    SslMode = SslMode.Require
};

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        npgsqlBuilder.ToString(),
        npgsqlOptions => npgsqlOptions
            .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Application)
    ).UseSnakeCaseNamingConvention()
);

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
