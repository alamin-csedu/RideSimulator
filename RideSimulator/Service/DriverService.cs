using Microsoft.AspNetCore.Http;
using RideSimulator.Data;
using RideSimulator.Models.DTO;
using System.Security.Claims;

namespace RideSimulator.Service
{
    public class DriverService : IDriverService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DriverService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor; 

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
