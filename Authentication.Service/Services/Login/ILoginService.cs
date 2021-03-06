﻿using System.Threading.Tasks;
using Authentication.Utilities.Models;

namespace Authentication.Service.Services.Login
{
    public interface ILoginService
    {
        Task<LoginResponse> LoginUsingPassword(LoginRequest loginRequest, string issuer);
        Task<LoginResponse> LoginUsingGoogle(LoginRequest loginRequest, string issuer);
        Task<LoginResponse> LoginUsingFacebook(LoginRequest loginRequest, string issuer);
    }
}