using System;
using System.Threading.Tasks;

namespace Authentication.Service.Repositories
{
    public interface IValidRefreshTokenRepository
    {
        Task<bool> Save(string key, TimeSpan expiry);
        Task<bool> TryGet(string key);
        Task<bool> Delete(string key);
    }
}