using Authentication.Model.Models;

namespace Authentication.Service.Services
{
    public interface IUserInfoManager
    {
        bool TryLoad(int userId, out UserInfo userInfo);
    }
}