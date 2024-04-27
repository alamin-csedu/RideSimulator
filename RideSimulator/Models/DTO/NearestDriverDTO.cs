using System.ComponentModel.DataAnnotations;

namespace RideSimulator.Models.DTO
{
    public class NearestDriverDTO
    {
        public DriverUser Driver { get; set; }

        public double EstimateTimeInMenutes { get; set; }

        public double Distance { get; set; }
    }
}
