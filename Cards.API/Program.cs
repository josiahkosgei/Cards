using Cards.API.Extensions;
using Cards.Core;
using Cards.Core.Services.Interfaces;
using Cards.Core.Services;
using Cards.Data;
using Cards.Data.Helpers.Migration;
using Cards.Data.IRepository;
using Cards.Data.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configure application settings
var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


string connectionString = builder.Configuration.GetConnectionString("AppConnectionString");


// Register Project Services
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
{
    opt.UseSqlServer(connectionString, builder =>
    {
        builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
    });
},
    contextLifetime: ServiceLifetime.Scoped,
    optionsLifetime: ServiceLifetime.Scoped
);
ServiceCollectionExtension.AddApplicationServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//Migrate Database and Seed Initial Data
app.MigrateDatabase<ApplicationDbContext>((context, services) =>
{
    var logger = services.GetService<ILogger<ApplicationDbContextSeed>>();
    ApplicationDbContextSeed.SeedAsync(context, logger).Wait();
}).Run();
