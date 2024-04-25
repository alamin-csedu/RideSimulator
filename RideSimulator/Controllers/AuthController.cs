using Microsoft.AspNetCore.Mvc;
using Pathao.Models.DTO;
using Pathao.Service;
namespace Pathao.Controllers;


[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register/rider")]
    public async Task<IActionResult> RegisterRider(RiderDto rider)
    {
        var result = await _authService.RegisterRiderAsync(rider);
        if (result.PhoneNumber is null)
        {
            return BadRequest(result);
        }
        return Created("", result);
    }

    [HttpPost("register/driver")]
    public async Task<IActionResult> RegisterDriver(DriverDto driver)
    {
        var result = await _authService.RegisterDriverAsync(driver);
        if(result.PhoneNumber is null)
        {
            return BadRequest(result);
        }
        return Created("",result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(string phone, string password)
    {
        var result = await _authService.LoginAsync(phone, password);
        if (!string.IsNullOrEmpty(result))
            return Ok(new { token = result });
        else
            return Unauthorized();
    }
}
