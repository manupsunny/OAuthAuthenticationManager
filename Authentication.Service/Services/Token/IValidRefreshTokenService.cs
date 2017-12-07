using System.Threading.Tasks;
using Authentication.Utilities.Models;

namespace Authentication.Service.Services.Token
{
    public interface IValidRefreshTokenService
    {
        Task<bool> Save(RefreshToken token);
        Task<bool> Extend(RefreshToken token);
        Task<bool> IsValid(RefreshToken token);
        Task<bool> Delete(RefreshToken token);
    }
}