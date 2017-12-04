using System;
using System.Threading.Tasks;

namespace Authentication.Service.Repositories
{
    [AutofacRegister(RegisterType.Singleton)]
    public sealed class ValidRefreshTokenRepository : IValidRefreshTokenRepository
    {
        public Task<bool> Save(string key, TimeSpan expiry)
        {
            throw new NotImplementedException();
        }

        public Task<bool> TryGet(string key)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(string key)
        {
            throw new NotImplementedException();
        }
    }
}