using System.Threading.Tasks;
using Authentication.Utilities.Models;

namespace Authentication.Service.Services.Login
{
    public interface ILoginService
    {
        Task<LoginResponse> Login(LoginRequest loginRequest, string issuer);
    }
}