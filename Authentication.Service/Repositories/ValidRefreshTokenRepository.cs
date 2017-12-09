using System;
using System.Threading.Tasks;
using Authentication.Utilities.Models;
using StackExchange.Redis;

namespace Authentication.Service.Repositories
{
    [AutofacRegister(RegisterType.Singleton)]
    public sealed class ValidRefreshTokenRepository : RedisRepository, IValidRefreshTokenRepository
    {
        public async Task<bool> Save(string key, TimeSpan expiry)
        {
            return await Task.Run(() =>
            {
                if (null == key)
                    throw new ArgumentNullException(nameof(key));

                var prefixedKey = GetPrefixedKey(key);
                var redisDb = GetRedisDatabase();

                redisDb.StringSet(prefixedKey, key, expiry);
                return true;
            });
        }

        public async Task<bool> TryGet(string key)
        {
            return await Task.Run(() =>
            {
                var success = false;
                var redisDb = GetRedisDatabase();
                var prefixedKey = GetPrefixedKey(key);
                var objFromRedis = redisDb.StringGet(prefixedKey);

                if (!objFromRedis.IsNull)
                    success = true;

                return success;
            });
        }

        public async Task<bool> Delete(string key)
        {
            return await Task.Run(() =>
            {
                var redisDb = GetRedisDatabase();
                var prefixedKey = GetPrefixedKey(key);

                redisDb.KeyDelete(prefixedKey);
                return true;
            });
        }

        private static IDatabase GetRedisDatabase()
        {
            return GetRedisDatabase(RedisNodes.ValidRefreshTokenNode);
        }

        private static string GetPrefixedKey(string key)
        {
            return $"refresh_token_{key}";
        }
    }
}