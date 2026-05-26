namespace DroneFleetAPI.Algorithms
{
    public class DroneRiskAlgorithm
    {
        public static int CalculateRiskScore(
           int battery,
           double speed,
           double altitude)
        {
            int risk = 0;

            // Battery
            if (battery < 20)
                risk += 40;
            else if (battery < 40)
                risk += 20;

            // Speed
            if (speed > 120)
                risk += 30;
            else if (speed > 80)
                risk += 15;

            // Altitude
            if (altitude > 500)
                risk += 30;
            else if (altitude > 300)
                risk += 15;

            return risk;
        }

        public static string CalculateStatus(int riskScore)
        {
            if (riskScore >= 60)
                return "Critical";

            if (riskScore >= 30)
                return "Warning";

            return "Safe";
        }
    }
}
