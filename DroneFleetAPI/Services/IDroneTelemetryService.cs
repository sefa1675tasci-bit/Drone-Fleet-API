using DroneFleetAPI.DTOs;
using DroneFleetAPI.Models;

namespace DroneFleetAPI.Services
{
    public interface IDroneTelemetryService
    {
        Task<DroneTelemetry> CreateTelemetryAsync(CreateDroneTelemetryDto dto);

        Task<List<DroneTelemetry>> GetTelemetryHistoryAsync();
    }
}