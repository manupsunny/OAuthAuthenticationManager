using System.Threading.Tasks;
using Authentication.Utilities.Models;

namespace Authentication.Service.Repositories
{
    public interface IAccessTokenLogRepository
    {
        Task<int> Save(UserTokenLog userTokenLog);
    }
}