namespace RideSimulator.Models
{
    public class RideRequest
    {
        public Guid ID { get; set; }
        public string RiderId { get; set; }
        public int RiderCurrentLongtitue { get; set; }
        public int RiderCurrentLattitude { get; set; }
        public int RiderDestinationLongtitude { get; set; }
        public int RiderDestinationLattitude { get; set; }
        public string RequestedDriver { get; set; }
        public enum Status
        {
            CancelledByRider, CancelledByDriver, Completed
        };
        public double Far { get; set; }
        public double ratingByRider { get; set; }
        public double ratingByDriver { get; set; }
    }
}
