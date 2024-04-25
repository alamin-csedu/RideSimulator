using AuthAPI.Models.DTO;
using Microsoft.AspNetCore.Identity;
using Pathao.Data;
using Pathao.Models;
using Pathao.Models.DTO;

namespace Pathao.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        //private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(AppDbContext dbContext, RoleManager<IdentityRole> roleManager,
           UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;

        }

        public Task<string> LoginAsync(string phone, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<LoginResponseDto> RegisterDriverAsync(DriverDto driver)
        {
            ApplicationUser user = new()
            {
                PhoneNumber = driver.PhoneNumber,
                UserName = driver.PhoneNumber,
                FullName = driver.FirstName + " " + driver.LastName,
                
            };
            var result = await _userManager.CreateAsync(user);
            try
            {
                if (result.Succeeded)
                {
                    var createdUser = _dbContext.Users.Where(u => u.PhoneNumber == driver.PhoneNumber).FirstOrDefault();
                    LoginResponseDto responseDto = new LoginResponseDto()
                    {
                        PhoneNumber = createdUser.PhoneNumber,
                        UserId = createdUser.Id,
                        Type = "Driver"
                    };
                    DriverUser driverUser = new()
                    {
                        Address = driver.Address,
                        BikeCC = driver.BikeCC,
                        BikeName = driver.BikeName,
                        FirstName  = driver.FirstName,
                        LastName = driver.LastName,
                        UserId = createdUser.Id
                    };
                    _dbContext.Drivers.Add(driverUser);
                    await _dbContext.SaveChangesAsync();
                    return responseDto;
                }
                else
                {
                    return new LoginResponseDto()
                    {
                        Message = result.Errors.ToList().FirstOrDefault().Description
                    };
                }
            }
            catch (Exception ex)
            {
                return new LoginResponseDto()
                {
                    Message = ex.Message,
                };
            }

        }

        public async Task<LoginResponseDto> RegisterRiderAsync(RiderDto rider)
        {
            ApplicationUser user = new()
            {
                PhoneNumber = rider.PhoneNumber,
                UserName = rider.PhoneNumber,
                Email = rider.Email,
                FullName = rider.FirstName + " " + rider.LastName,
            };
            var result = await _userManager.CreateAsync(user);
            try
            {
                if (result.Succeeded)
                {
                    var createdUser = _dbContext.Users.Where(u => u.PhoneNumber == rider.PhoneNumber).FirstOrDefault();
                    LoginResponseDto responseDto = new LoginResponseDto()
                    {
                        PhoneNumber = createdUser.PhoneNumber,
                        UserId = createdUser.Id,
                        Type = "Rider"
                    };
                    RiderUser riderUser = new()
                    {
                        Address = rider.Address,
                        FirstName = rider.FirstName,
                        LastName = rider.LastName,
                        UserId = createdUser.Id
                    };
                    _dbContext.Riders.Add(riderUser);
                    await _dbContext.SaveChangesAsync();
                    return responseDto;
                }
                else
                {
                    return new LoginResponseDto()
                    {
                        Message = result.Errors.ToList().FirstOrDefault().Description
                    };
                }
            }
            catch (Exception ex)
            {
                return new LoginResponseDto()
                {
                    Message = ex.Message,
                };
            }
        }
    }
}
