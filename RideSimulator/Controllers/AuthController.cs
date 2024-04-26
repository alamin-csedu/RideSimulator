using AuthAPI.Models.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RideSimulator.Data;
using RideSimulator.Models;
using RideSimulator.Models.DTO;
using RideSimulator.Service;
namespace RideSimulator.Controllers;


[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly INotificationService _notificationService;
    private readonly IAuthService _authService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    //private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthController(UserManager<ApplicationUser> userManager,
       INotificationService notificationService, IAuthService authService, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _notificationService = notificationService;
        _authService = authService;
        _httpContextAccessor = httpContextAccessor;
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
    public async Task<IActionResult> Login(string phone)
    {
        try
        {
            var user = await _userManager.FindByNameAsync(phone);
            if (user is not null)
            {
                var token = await _userManager.GenerateTwoFactorTokenAsync(user, "Phone");
                await _notificationService.SendOTPAsync(phone, token);
                _httpContextAccessor.HttpContext.Session.SetString(phone+token.ToString(), token);
                return Ok(new { Message = "OTP send successfully to your phone" , otp = token});
            }
            return BadRequest(new { Message = "Something wrong" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = "Something wrong" });
        }
    }

    [HttpPost("login/{otp}")]
    public async Task<IActionResult> LoginWithCode(string phone,string otp)
    {
        _httpContextAccessor.HttpContext.Session.TryGetValue(phone+otp, out var value);
        if (value is not null)
        {
            return Ok(new { Message = "Logged in successfully" });
        }
        else
        {
            return BadRequest(new { Message = "Something wrong" });
        }
    }
}
