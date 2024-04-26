using RideSimulator.Models;
using RideSimulator.Models.DTO;

namespace RideSimulator.Service
{
    public interface IDriverService
    {
        Task<bool> PingDriverLocation(DriverLocationDto driverLocationDto);
        Task<bool> DriverActions(Guid requestId, Status status);
    }
}
