using System.Threading.Tasks;
using Authentication.Models.Models;

namespace Authentication.Service.Repositories
{
    public interface IAccessTokenLogRepository
    {
        Task<int> Save(UserTokenLog userTokenLog);
    }
}