using System;
using System.Collections.Generic;
using Authentication.Utilities.Models;

namespace Authentication.Service.Repositories
{
    [AutofacRegister(RegisterType.Singleton)]
    public class RefreshTokenLogRepository : IRefreshTokenLogRepository
    {
        private readonly List<UserTokenLog> refreshTokenLogs = new List<UserTokenLog>();

        public void Save(UserTokenLog userTokenLog)
        {
            Console.WriteLine("AccessToken Issued");
            Console.WriteLine($"TokenId = {userTokenLog.TokenId}, " +
                              $"UserId = {userTokenLog.UserId}, " +
                              $"Channel = {userTokenLog.Channel}, " +
                              $"StartTime = {userTokenLog.StartTime}, " +
                              $"EndTime = {userTokenLog.EndTime}");

            refreshTokenLogs.Add(userTokenLog);
            // Save this to a persistent storage for future reference
        }

        public UserTokenLog Find(string tokenId)
        {
            return refreshTokenLogs.Find(x => x.TokenId.Equals(tokenId));
        }
    }
}