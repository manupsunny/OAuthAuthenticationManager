using Authentication.Models.Models;

namespace Authentication.Service.Services.Login
{
    public interface ILoginManager
    {
        bool LoginPasswordMatch(string loginRequestUserName, Password password, out int userId);
    }
}