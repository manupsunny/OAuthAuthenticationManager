using System.Threading.Tasks;
using Authentication.Model.Models;

namespace Authentication.Service.Repositories
{
    public interface IAccessTokenLogRepository
    {
        Task<int> Save(UserTokenLog userTokenLog);
    }
}