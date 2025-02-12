using FootballersCatalog.Api.Abstract;
using FootballersCatalog.Api.Services;
using FootballersCatalog.Persistence;
using FootballersCatalog.Persistence.Abstract;
using FootballersCatalog.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbPort = Environment.GetEnvironmentVariable("DB_PORT");
var dbUser = Environment.GetEnvironmentVariable("DB_USER");
var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
var dbName = Environment.GetEnvironmentVariable("DB_NAME");

var connectionString = $"Host={dbHost};Port={dbPort};Username={dbUser};Password={dbPassword};Database={dbName};";

builder.Services.AddDbContext<DatabaseContext>(
    options =>
    {
        //options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(DatabaseContext)));
        options.UseNpgsql(connectionString);
    });

builder.Services.AddScoped<IFootballersService, FootballersService>();
builder.Services.AddScoped<IFootballersRepository, FootballersRepository>();
builder.Services.AddScoped<ITeamsService, TeamsService>();
builder.Services.AddScoped<ITeamsRepository, TeamsRepository>();
builder.Services.AddAutoMapper(typeof(AppMappingProfile));

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowAll",
        builder =>
        {
            builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

var app = builder.Build();
app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<DatabaseContext>();
    dbContext.Database.Migrate();
}

app.Run();
