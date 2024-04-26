using RideSimulator.Models.DTO;

namespace RideSimulator.Service
{
    public interface IDriverService
    {
        Task<bool> PingDriverLocation(DriverLocationDto driverLocationDto);
    }
}
