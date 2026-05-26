using Microsoft.EntityFrameworkCore;
using DroneFleetAPI.Models;

namespace DroneFleetAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<DroneTelemetry> DroneTelemetryData { get; set; }
    }
}