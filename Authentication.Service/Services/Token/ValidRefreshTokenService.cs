﻿using System;
using System.Threading.Tasks;
using Authentication.Service.Repositories;
using Authentication.Utilities.Common;
using Authentication.Utilities.Models;

namespace Authentication.Service.Services.Token
{
    [AutofacRegister(RegisterType.Singleton)]
    public class ValidRefreshTokenService : IValidRefreshTokenService
    {
        private readonly IValidRefreshTokenRepository ValidRefreshTokenRepository;

        public ValidRefreshTokenService(IValidRefreshTokenRepository validRefreshTokenRepository)
        {
            ValidRefreshTokenRepository = validRefreshTokenRepository;
        }

        public async Task<bool> Save(RefreshToken token)
        {
            var expiry = TimeSpan.FromDays(double.Parse(ApplicationSettings.RefreshTokenValidityInDays));
            return await ValidRefreshTokenRepository.Save(CreateRedisKeyFromToken(token), expiry);
        }

        public async Task<bool> Extend(RefreshToken token)
        {
            var exists = await IsValid(token);
            if (exists)
            {
                await Delete(token);
                return await Save(token);
            }
            return false;
        }

        public async Task<bool> IsValid(RefreshToken token)
        {
            return await ValidRefreshTokenRepository.TryGet(CreateRedisKeyFromToken(token));
        }

        public async Task<bool> Delete(RefreshToken token)
        {
            return await ValidRefreshTokenRepository.Delete(CreateRedisKeyFromToken(token));
        }

        private static string CreateRedisKeyFromToken(UserToken token)
        {
            return $"{token.UserID}_{token.ConsumerKey.Channel}_{token.TokenID}";
        }
    }
}