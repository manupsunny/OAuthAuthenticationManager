using System;
using Authentication.Models.Models;

namespace Authentication.Service.Services.Login
{
    [AutofacRegister(RegisterType.Singleton)]
    class LoginManager : ILoginManager
    {
        public bool LoginPasswordMatch(string loginRequestUserName, Password password, out int userId)
        {
            throw new NotImplementedException();
        }
    }
}