using System;
using System.Threading.Tasks;
using Authentication.Model.Models;

namespace Authentication.Service.Repositories
{
    [AutofacRegister(RegisterType.Singleton)]
    public class RefreshTokenLogRepository : IRefreshTokenLogRepository
    {
        public Task<int> Save(UserTokenLog userTokenLog)
        {
            throw new NotImplementedException();
        }

        public Task<UserTokenLog> Find(string tokenId)
        {
            throw new NotImplementedException();
        }
    }
}