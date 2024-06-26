﻿using AuthAPI.Models.DTO;
using RideSimulator.Models.DTO;

namespace RideSimulator.Service
{
    public interface IAuthService
    {
        Task<LoginResponseDto> RegisterRiderAsync(RiderDto rider);
        Task<LoginResponseDto> RegisterDriverAsync(DriverDto driver);
    }
}
