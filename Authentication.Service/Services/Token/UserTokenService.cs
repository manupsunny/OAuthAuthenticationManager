using System.Threading.Tasks;
using Authentication.Service.Services.Log;
using Authentication.Service.Utilities;
using Authentication.Utilities.Models;

namespace Authentication.Service.Services.Token
{
    [AutofacRegister(RegisterType.Singleton)]
    public class UserTokenService : IUserTokenService
    {
        private readonly IAccessTokenLogService AccessTokenLogService;
        private readonly IRefreshTokenLogService RefreshTokenLogService;
        private readonly IValidRefreshTokenService ValidRefreshTokenService;

        public UserTokenService(IAccessTokenLogService accessTokenLogService,
            IRefreshTokenLogService refreshTokenLogService, IValidRefreshTokenService validRefreshTokenService)
        {
            AccessTokenLogService = accessTokenLogService;
            RefreshTokenLogService = refreshTokenLogService;
            ValidRefreshTokenService = validRefreshTokenService;
        }

        public async Task<RefreshToken> CreateRefreshToken(string userId, string userName, Role role, string profileId,
            ConsumerKey consumerKey, string issuer)
        {
            var refreshTokenId = IDUtility.GenerateId();

            var refreshToken = new RefreshToken(userId, profileId, userName, role.ToString(), consumerKey,
                refreshTokenId, issuer);
            var refreshTokenLog = new UserTokenLog(refreshToken);

            await ValidRefreshTokenService.Save(refreshToken);
            await RefreshTokenLogService.Save(refreshTokenLog);
            return refreshToken;
        }

        public async Task<bool> ExpireRefreshToken(RefreshToken token)
        {
            return await ValidRefreshTokenService.Delete(token);
        }

        public async Task<bool> IsRefreshTokenValid(RefreshToken token)
        {
            return await ValidRefreshTokenService.IsValid(token);
        }

        public async Task<AccessToken> CreateAnonymousAccessToken(ConsumerKey consumerKey, string issuer)
        {
            var accessTokenId = IDUtility.GenerateId();
            var accessToken =
                new AccessToken("", "", "", Role.ANONYMOUS.ToString(), consumerKey, accessTokenId, issuer);
            var accessTokenLog = new UserTokenLog(accessToken);

            await AccessTokenLogService.Save(accessTokenLog);
            return accessToken;
        }

        public async Task<AccessToken> CreateAccessToken(RefreshToken refreshToken)
        {
            var accessTokenId = IDUtility.GenerateId();
            var accessToken = new AccessToken(refreshToken.UserID, refreshToken.ProfileID, refreshToken.UserName,
                refreshToken.Role.ToString(), refreshToken.ConsumerKey, accessTokenId, refreshToken.JwtIssuer);
            var accessTokenLog = new UserTokenLog(accessToken);

            await AccessTokenLogService.Save(accessTokenLog);
            return accessToken;
        }
    }
}