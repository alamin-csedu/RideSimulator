using RideSimulator.Models;
using RideSimulator.Models.DTO;

namespace RideSimulator.Service
{
    public interface IRiderService
    {
        Task<RideRequest> RequestRide(RequestRideDTO requestDTO);
        Task<List<NearestDriverDTO>> NearestDriver(Guid rideRequestId);
        Task<RideRequest> ConfirmDriver(Guid requestId, Guid driverID);
    }
}
