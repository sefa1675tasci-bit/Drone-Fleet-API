namespace DroneFleetAPI.Models
{
    public class DroneTelemetry
    {
        public int Id { get; set; }

        public string DroneCode { get; set; } = string.Empty;

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public int Battery { get; set; }

        public double Speed { get; set; }

        public double Altitude { get; set; }

        public int RiskScore { get; set; }

        public string Status { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
