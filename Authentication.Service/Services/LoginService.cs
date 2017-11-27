using Authentication.Service.Models;

namespace Authentication.Service.Services
{
    public class LoginService : ILoginService
    {
        public bool Login(LoginRequest loginRequest, string issuer)
        {
            return loginRequest.userName.Equals("valid_user") &&
                   loginRequest.password.Equals("valid_password") &&
                   issuer.Equals("valid_issuer");
        }
    }
}