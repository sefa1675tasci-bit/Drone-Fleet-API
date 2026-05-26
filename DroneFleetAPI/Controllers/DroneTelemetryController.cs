using DroneFleetAPI.DTOs;
using DroneFleetAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DroneFleetAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DroneTelemetryController : ControllerBase
    {
        private readonly IDroneTelemetryService _telemetryService;

        public DroneTelemetryController(IDroneTelemetryService telemetryService)
        {
            _telemetryService = telemetryService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateDroneTelemetryDto dto)
        {
            var result = await _telemetryService.CreateTelemetryAsync(dto);

            return Ok(result);
        }

        [HttpGet("history")]
        public async Task<IActionResult> History()
        {
            var data = await _telemetryService.GetTelemetryHistoryAsync();

            return Ok(data);
        }
    }
}