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

builder.Services.AddDbContext<DatabaseContext>(
    options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(DatabaseContext)));
    });

builder.Services.AddScoped<IFootballersService, FootballersService>();
builder.Services.AddScoped<IFootballersRepository, FootballersRepository>();
builder.Services.AddScoped<ITeamsService, TeamsService>();
builder.Services.AddScoped<ITeamsRepository, TeamsRepository>();
builder.Services.AddAutoMapper(typeof(AppMappingProfile));

// builder.Services.AddCors(options =>
// {
//     options.AddPolicy("AllowLocalhost5173",
//         policy => policy.WithOrigins("http://localhost:5173")
//                         .AllowAnyMethod()
//                         .AllowAnyHeader());
// });

builder.Services.AddCors(options =>
{
    if (builder.Environment.IsDevelopment())
    {
        options.AddDefaultPolicy(
            corsPolicyBuilder =>
            {
                corsPolicyBuilder.SetIsOriginAllowed(origin => new Uri(origin).IsLoopback)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
    }
    else
    {
        options.AddDefaultPolicy(
            corsPolicyBuilder =>
            {
                corsPolicyBuilder.WithOrigins(builder.Configuration.GetSection("Domain").Get<string[]>() ?? [])
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
    }
});

var app = builder.Build();
app.UseCors();

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
