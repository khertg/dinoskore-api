using DinoSkore.Api.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Options;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("DatabaseSettings"));

builder.Services.AddDbContext<AppDbContext>((sp, options) =>
{
    var dbSettings = sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;

    var npgsqlBuilder = new Npgsql.NpgsqlConnectionStringBuilder
    {
        Host = dbSettings.Host,
        Port = dbSettings.Port,
        Username = dbSettings.Username,
        Password = dbSettings.Password,
        Database = dbSettings.Database,
        SslMode = dbSettings.UseSsl ? Npgsql.SslMode.Require : Npgsql.SslMode.Disable
    };

    options.UseNpgsql(npgsqlBuilder.ToString());
});


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
