using DroneFleetAPI.Data;
using DroneFleetAPI.Hubs;
using DroneFleetAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddSignalR();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

// SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=dronefleet.db"));

// Services
builder.Services.AddScoped<IDroneTelemetryService, DroneTelemetryService>();

var app = builder.Build();

// Swagger
app.UseSwagger();

app.UseSwaggerUI();
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHub<DroneHub>("/droneHub");

app.Run();