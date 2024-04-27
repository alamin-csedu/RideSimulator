namespace RideSimulator.Models
{
    public class RideRequest
    {
        public Guid ID { get; set; }

        public int RiderPickupLongtitue { get; set; }

        public int RiderPickLattitude { get; set; }

        public int RiderDestinationLongtitude { get; set; }

        public int RiderDestinationLattitude { get; set; }
       
        public double? Far { get; set; }

        public Status? Status { get; set; }

        public double? ratingByRider { get; set; }

        public double? ratingByDriver { get; set; }

        public Guid? RiderId { get; set; }

        public RiderUser RiderUser { get; set; }

        public Guid? RequestedDriverId { get; set; }

        public DriverUser DriverUser { get; set; }
    }

    public enum Status
    {
        CancelledByRider, CancelledByDriver, Completed, AcceptedByDriver, PendingRequest
    };
}
