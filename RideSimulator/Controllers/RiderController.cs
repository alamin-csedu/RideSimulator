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
    public class RiderController : ControllerBase
    {
        private readonly IRiderService _riderService;
        public RiderController(IRiderService riderService)
        {
            _riderService = riderService;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Rider")]
        [HttpPost("request-ride")]
        public async Task<IActionResult> RequestRide(RequestRideDTO requestRide)
        {
            var result = await _riderService.RequestRide(requestRide);
            return Ok(result);
        }

       
        [HttpPost("nearest-driver")]
        public async Task<IActionResult> NearestDriver(Guid requestRideId)
        {
            var result = await _riderService.NearestDriver(requestRideId); 
            return Ok(result);
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Rider")]
        [HttpGet("confirm-rider")]
        public async Task<IActionResult> ConfirmDriver(Guid requestId, Guid driverId)
        {
            var result = await _riderService.ConfirmDriver(requestId,driverId);
            if(result is null)
            {
                return Ok(new { Message = "Rider is anavailable at this moment" });
            }
            return Ok(result);
        }


    }
}
