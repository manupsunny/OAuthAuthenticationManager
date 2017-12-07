using System.Threading.Tasks;
using Authentication.Utilities.Models;

namespace Authentication.Service.Services.Token
{
    public interface IUserTokenService
    {
        Task<AccessToken> CreateAccessToken(RefreshToken refreshToken);

        Task<RefreshToken> CreateRefreshToken(string userId, string userName, Role role, string profileId,
            ConsumerKey consumerKey, string issuer);

        Task<bool> ExpireRefreshToken(RefreshToken token);

        Task<bool> IsRefreshTokenValid(RefreshToken token);

        Task<AccessToken> CreateAnonymousAccessToken(ConsumerKey consumerKey, string issuer);
    }
}