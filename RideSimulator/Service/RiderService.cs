using RideSimulator.Data;
using RideSimulator.Models;
using RideSimulator.Models.DTO;
using System.Security.Claims;

namespace RideSimulator.Service
{
    public class RiderService : IRiderService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public RiderService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;

        }

        public async Task<RideRequest> ConfirmDriver(Guid requestId, Guid driverID)
        {
            var rideRequest = _context.RideRequests.Where(p => p.ID == requestId).FirstOrDefault();
            if (rideRequest != null)
            {
                rideRequest.RequestedDriverId = driverID;
                rideRequest.Status = Status.PendingRequest;
            }
             _context.SaveChanges();
            var driverUser = _context.Drivers.FirstOrDefault(p => p.ID == driverID);
            var riderUser = _context.Riders.FirstOrDefault(p => p.ID == rideRequest.RiderId);
            rideRequest.RiderUser = riderUser;
            rideRequest.DriverUser = driverUser;
            return rideRequest;
        }

        public async Task<List<NearestDriverDTO>> NearestDriver(Guid rideRequestId)
        {
            var rideRequest = _context.RideRequests.Where(p => p.ID == rideRequestId).FirstOrDefault();
            var totalAvailAbleDriver = _context.Drivers.Where(p => p.IsOnline).Select(
                el => new
                {
                    distance = Math.Sqrt((double)rideRequest.RiderPickLattitude * (double)rideRequest.RiderPickupLongtitue +
                    (double)el.CurrentLogtitude * (double)el.CurrentLattitude),
                    Driver = el
                    
                }
                ).OrderBy(k => k.distance).ToList().Take(3);
            List<NearestDriverDTO> nearestDrivers = new List<NearestDriverDTO>();

            foreach(var item in totalAvailAbleDriver)
            {
                nearestDrivers.Add(new NearestDriverDTO()
                {
                   Driver = item.Driver,
                   EstimateTimeInMenutes = item.distance*3,
                   Distance = item.distance
                });
            }
            return nearestDrivers;
        }

        public async Task<RideRequest> RequestRide(RequestRideDTO requestDTO)
        {
            string riderUserId = "";
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                riderUserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }
            Guid.TryParse(riderUserId, out  Guid riderUsetId);
            var riderId = _context.Riders.FirstOrDefault(p => p.UserId == riderUserId).ID;
            RideRequest rideRequest = new RideRequest()
            {
                RiderPickLattitude = requestDTO.RiderPickLattitude,
                RiderPickupLongtitue = requestDTO.RiderPickupLongtitue,
                RiderDestinationLattitude = requestDTO.RiderDestinationLattitude,
                RiderDestinationLongtitude = requestDTO.RiderDestinationLongtitude,
                RiderId = riderId,
            };

             _context.Add(rideRequest);
            await _context.SaveChangesAsync();

            return _context.RideRequests.FirstOrDefault(p => p.RiderId == riderId);
        }
    }
}
