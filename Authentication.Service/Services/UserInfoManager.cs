using System;
using Authentication.Models.Models;

namespace Authentication.Service.Services
{
    [AutofacRegister(RegisterType.Singleton)]
    class UserInfoManager : IUserInfoManager
    {
        public bool TryLoad(int userId, out UserInfo userInfo)
        {
            throw new NotImplementedException();
        }
    }
}