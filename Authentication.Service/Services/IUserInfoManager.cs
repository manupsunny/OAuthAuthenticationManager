using Authentication.Utilities.Models;

namespace Authentication.Service.Services
{
    public interface IUserInfoManager
    {
        bool TryLoad(int userId, out UserInfo userInfo);
    }
}