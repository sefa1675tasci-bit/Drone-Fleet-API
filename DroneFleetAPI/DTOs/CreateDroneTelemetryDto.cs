namespace DroneFleetAPI.DTOs
{
    public class CreateDroneTelemetryDto
    {
        public string DroneCode { get; set; } = string.Empty;

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public int Battery { get; set; }

        public double Speed { get; set; }

        public double Altitude { get; set; }
    }
}
