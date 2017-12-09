using Authentication.Utilities.Exceptions;
using Authentication.Utilities.Models;

namespace Authentication.Service.Services.Login
{
    [AutofacRegister(RegisterType.Singleton)]
    class LoginManager : ILoginManager
    {
        public bool LoginPasswordMatch(string userName, Password password, out int userId)
        {
            // Check using user data from database

            if (userName == "admin@msp.com" && password.Value() == "adminPassword")
            {
                userId = 1;
                return true;
            }

            if (userName == "user@msp.com" && password.Value() == "userPassword")
            {
                userId = 0;
                return true;
            }

            throw new UnauthorizedException("User not found!");
        }
    }
}