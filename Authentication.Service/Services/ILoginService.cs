using Authentication.Service.Models;

namespace Authentication.Service.Services
{
    public interface ILoginService
    {
        bool Login(LoginRequest loginRequest, string issuer);
    }
    
}
