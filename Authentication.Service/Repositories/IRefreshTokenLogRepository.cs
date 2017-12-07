using System.Threading.Tasks;
using Authentication.Utilities.Models;

namespace Authentication.Service.Repositories
{
    public interface IRefreshTokenLogRepository
    {
        Task<int> Save(UserTokenLog userTokenLog);
        Task<UserTokenLog> Find(string tokenId);
    }
}