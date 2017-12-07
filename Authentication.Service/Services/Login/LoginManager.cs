using System;
using Authentication.Utilities.Models;

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