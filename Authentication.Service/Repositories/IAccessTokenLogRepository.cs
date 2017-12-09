using Authentication.Utilities.Models;

namespace Authentication.Service.Repositories
{
    public interface IAccessTokenLogRepository
    {
        void Save(UserTokenLog userTokenLog);
        UserTokenLog Find(string tokenId);
    }
}