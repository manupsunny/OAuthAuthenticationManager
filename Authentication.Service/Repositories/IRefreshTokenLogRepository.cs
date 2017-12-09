using Authentication.Utilities.Models;

namespace Authentication.Service.Repositories
{
    public interface IRefreshTokenLogRepository
    {
        void Save(UserTokenLog userTokenLog);
        UserTokenLog Find(string tokenId);
    }
}