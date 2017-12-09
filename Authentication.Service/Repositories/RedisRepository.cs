using System.Collections.Generic;
using System.ComponentModel;
using Authentication.Utilities.Common;
using Authentication.Utilities.Models;
using StackExchange.Redis;

namespace Authentication.Service.Repositories
{
    public abstract class RedisRepository
    {
        private static readonly Dictionary<RedisNodes, ConnectionMultiplexer> NodeConnections =
            new Dictionary<RedisNodes, ConnectionMultiplexer>();

        private static readonly object syncLock = new object();

        protected static IDatabase GetRedisDatabase(RedisNodes redisNode)
        {
            if (!NodeConnections.TryGetValue(redisNode, out var redis) || redis != null && !redis.IsConnected)
            {
                lock (syncLock)
                {
                    if (!NodeConnections.TryGetValue(redisNode, out redis) || redis != null && !redis.IsConnected)
                    {
                        if (redis != null)
                        {
                            NodeConnections.Remove(redisNode);
                            redis.Close();
                        }

                        var connectionString = GetConnectionString(redisNode);
                        var redisConnection = ConnectionMultiplexer.Connect(connectionString);

                        NodeConnections.Add(redisNode, redisConnection);
                        redis = redisConnection;
                    }
                }
            }

            return redis?.GetDatabase((int) redisNode);
        }

        private static ConfigurationOptions GetConnectionString(RedisNodes redisNode)
        {
            ConfigurationOptions configurationOptions;
            switch (redisNode)
            {
                case RedisNodes.AccessTokenNode:
                    configurationOptions = ConfigurationOptions.Parse(ApplicationSettings.AccessTokenRedisHostName);
                    break;
                case RedisNodes.RefreshTokenNode:
                    configurationOptions = ConfigurationOptions.Parse(ApplicationSettings.RefreshTokenRedisHostName);
                    break;
                case RedisNodes.ValidRefreshTokenNode:
                    configurationOptions =
                        ConfigurationOptions.Parse(ApplicationSettings.ValidRefreshTokenRedisHostName);
                    break;
                default:
                    throw new InvalidEnumArgumentException(nameof(redisNode), (int) redisNode, typeof(RedisNodes));
            }
            configurationOptions.AbortOnConnectFail = false;
            return configurationOptions;
        }
    }
}