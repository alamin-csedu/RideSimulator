using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RideSimulator.Models;
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

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Driver")]
        [HttpPut("take-actions")]
        public async Task<IActionResult> DriverActions(Guid requestId, string actions)
        {
            bool result = false;
            if(actions == "Accept")
            {
                 result = await _driverService.DriverActions(requestId, Status.AcceptedByDriver);
                if (result)
                {
                    return Ok(new { Message = "You acceped ride request successfully" });
                }
            }
            else if(actions == "Complete")
            {
                result = await _driverService.DriverActions(requestId, Status.Completed);
                if (result)
                {
                    return Ok(new { Message = "You completed ride request successfully" });
                }
            }
            else if(actions == "Cancel")
            {
                result = await _driverService.DriverActions(requestId, Status.Completed);
                if (result)
                {
                    return Ok(new { Message = "You cancelled ride request successfully" });
                }
            }
            else
            {
                return BadRequest(new { Message = actions + " is not a actions" });
            }

            return Ok(new { Message = "Some this is wrong" });
        }
    }
}
