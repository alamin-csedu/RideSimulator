using Microsoft.AspNetCore.Http;
using RideSimulator.Data;
using RideSimulator.Models;
using RideSimulator.Models.DTO;
using System.Security.Claims;

namespace RideSimulator.Service
{
    public class DriverService : IDriverService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRiderService _riderService;
        public DriverService(AppDbContext context, IHttpContextAccessor httpContextAccessor, IRiderService riderService)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor; 
            _riderService = riderService;

        }

        public async Task<bool> DriverActions(Guid requestId, Status status)
        {
            try
            {
                var rideRequest = _context.RideRequests.FirstOrDefault(p => p.ID == requestId);
                rideRequest.Status = status;

                await _context.SaveChangesAsync();
                return true;
            }
           catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }


        }

        public async Task<bool> PingDriverLocation(DriverLocationDto driverLocationDto)
        {
            string driverUserId;
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                driverUserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }
            else
            {
                return false;
            }
            var user = _context.Drivers.Where(p => p.UserId == driverUserId).FirstOrDefault();
            try
            {
                if (user is not null)
                {
                    user.CurrentLattitude = driverLocationDto.Lattitude;
                    user.CurrentLogtitude = driverLocationDto.Logtitude;
                    user.IsOnline = true;
                    _context.Drivers.Update(user);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else { return false; }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
