using System;
using Authentication.Utilities.Models;

namespace Authentication.Service.Services
{
    [AutofacRegister(RegisterType.Singleton)]
    class UserInfoManager : IUserInfoManager
    {
        public bool TryLoad(int userId, out UserInfo userInfo)
        {
            if (userId == 0)
            {
                userInfo = new UserInfo
                {
                    IsAdmin = false,
                    IsUser = true
                };
                return true;
            }

            if (userId == 1)
            {
                userInfo = new UserInfo
                {
                    IsAdmin = true,
                    IsUser = true
                };
                return true;
            }

            userInfo = null;
            return false;
        }
    }
}