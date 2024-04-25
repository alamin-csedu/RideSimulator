using AuthAPI.Models.DTO;
using Pathao.Models.DTO;

namespace Pathao.Service
{
    public interface IAuthService
    {
        Task<LoginResponseDto> RegisterRiderAsync(RiderDto rider);
        Task<LoginResponseDto> RegisterDriverAsync(DriverDto driver);
        Task<string> LoginAsync(string phone, string password);
    }
}
