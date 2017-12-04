﻿using System;
using System.Threading.Tasks;
using Authentication.Model.Models;

namespace Authentication.Service.Repositories
{
    [AutofacRegister(RegisterType.Singleton)]
    public class AccessTokenLogRepository : IAccessTokenLogRepository
    {
        public Task<int> Save(UserTokenLog userTokenLog)
        {
            throw new NotImplementedException();
        }
    }
}