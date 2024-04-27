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
            try
            {
                var requestedRide = await _riderService.RequestRide(requestRide);
                return Ok(requestedRide);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
           
            
        }

       
        [HttpGet("nearest-driver/{requestRideId}")]
        public async Task<IActionResult> NearestDriver(Guid requestRideId)
        {
            if(requestRideId == Guid.Empty)
            {
                return BadRequest(new {Message = "RequestId can not be null"});
            }
            try
            {
                var topThreeNearestDriversSortedByDistance = await _riderService.NearestDriver(requestRideId);
                return Ok(topThreeNearestDriversSortedByDistance);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
           
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Rider")]
        [HttpGet("confirm-rider")]
        public async Task<IActionResult> ConfirmDriver(Guid requestId, Guid driverId)
        {
            var confirmedRideRequest = await _riderService.ConfirmDriver(requestId,driverId);
            if(confirmedRideRequest is null)
            {
                return Ok(new { Message = "Rider is unavailable at this moment" });
            }
            return Ok(confirmedRideRequest);
        }


    }
}
