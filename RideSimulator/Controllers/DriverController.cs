using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RideSimulator.Models.DTO;
using RideSimulator.Service;

namespace RideSimulator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        private readonly IDriverService _driverService;
        public DriverController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Driver")]
        [HttpPost("get-online-and-ping-location")]
        public async Task<IActionResult> PingLocation(DriverLocationDto driverLocation)
        {
            var isSuccess =await _driverService.PingDriverLocation(driverLocation);
            if (isSuccess)
            {
                return Ok(new { Message = "Your are online now and waiting for ride request" });
            }
            else
            {
                return BadRequest(new {Message = "Some thing is wrong, Please try again"});
            }
        }
    }
}
