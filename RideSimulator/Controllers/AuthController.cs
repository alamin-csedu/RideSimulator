using AuthAPI.Models.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RideSimulator.Data;
using RideSimulator.Models;
using RideSimulator.Models.DTO;
using RideSimulator.Service;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
namespace RideSimulator.Controllers;


[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly INotificationService _notificationService;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IConfiguration _configuration;
    private readonly IAuthService _authService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    //private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthController(UserManager<ApplicationUser> userManager,
       INotificationService notificationService, IAuthService authService, IHttpContextAccessor httpContextAccessor,
       SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _notificationService = notificationService;
        _authService = authService;
        _httpContextAccessor = httpContextAccessor;
        _signInManager = signInManager;
        _configuration = configuration; 
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
            var user = await _userManager.FindByNameAsync(phone);
            string token =await GetTokenAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            return Ok(new LoginResponseDto()
            {
                Token = token,
                PhoneNumber = phone,
                Type =  roles.FirstOrDefault()
            });
        }
        else
        {
            return BadRequest(new { Message = "Something wrong" });
        }
    }


    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok(new LoginResponseDto { Message = "Successfully Logout" });
    }

    private async Task<string> GetTokenAsync(ApplicationUser user)
    {
        var utcNow = DateTime.UtcNow;

        var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, utcNow.ToString())
            };

        var roles = await _userManager.GetRolesAsync(user);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("pathaobangladesh"));

        var jwt = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(Convert.ToDouble(_configuration["Jwt:ExpireDays"])),
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}
