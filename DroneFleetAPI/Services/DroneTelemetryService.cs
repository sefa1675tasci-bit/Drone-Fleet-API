using DroneFleetAPI.Algorithms;
using DroneFleetAPI.Data;
using DroneFleetAPI.DTOs;
using DroneFleetAPI.Hubs;
using DroneFleetAPI.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace DroneFleetAPI.Services
{
    public class DroneTelemetryService : IDroneTelemetryService
    {
        private readonly AppDbContext _context;

        private readonly IHubContext<DroneHub> _hub;

        public DroneTelemetryService(
            AppDbContext context,
            IHubContext<DroneHub> hub)
        {
            _context = context;
            _hub = hub;
        }

        public async Task<DroneTelemetry> CreateTelemetryAsync(
            CreateDroneTelemetryDto dto)
        {
            int riskScore =
                DroneRiskAlgorithm.CalculateRiskScore(
                    dto.Battery,
                    dto.Speed,
                    dto.Altitude
                );

            string status =
                DroneRiskAlgorithm.CalculateStatus(
                    riskScore
                );

            var existingDrone =
    await _context.DroneTelemetryData
        .FirstOrDefaultAsync(
            x => x.DroneCode == dto.DroneCode
        );
            if (existingDrone != null)
            {
                existingDrone.Latitude = dto.Latitude;
                existingDrone.Longitude = dto.Longitude;
                existingDrone.Battery = dto.Battery;
                existingDrone.Speed = dto.Speed;
                existingDrone.Altitude = dto.Altitude;
                existingDrone.RiskScore = riskScore;
                existingDrone.Status = status;
                existingDrone.CreatedAt = DateTime.Now;

                await _context.SaveChangesAsync();

                await _hub.Clients.All.SendAsync(
                    "ReceiveTelemetry",
                    existingDrone
                );

                return existingDrone;
            }
            var telemetry = new DroneTelemetry
            {
                DroneCode = dto.DroneCode,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                Battery = dto.Battery,
                Speed = dto.Speed,
                Altitude = dto.Altitude,
                RiskScore = riskScore,
                Status = status,
                CreatedAt = DateTime.Now
            };

            _context.DroneTelemetryData.Add(telemetry);

            await _context.SaveChangesAsync();

            // =========================
            // SIGNALR REALTIME
            // =========================

            await _hub.Clients.All.SendAsync(
                "ReceiveDroneTelemetry",
                telemetry
            );

            return telemetry;
        }

        public async Task<List<DroneTelemetry>>
            GetTelemetryHistoryAsync()
        {
            return await _context.DroneTelemetryData
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }
    }
}