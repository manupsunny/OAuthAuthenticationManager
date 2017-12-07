using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Authentication.Model.Models;

namespace Authentication.Model.Repositories
{
    public class ConsumerKeyRepository : IConsumerKeyRepository
    {
        private readonly List<ConsumerKey> consumerKeys = new List<ConsumerKey>
        {
            new ConsumerKey
            {
                Channel = "App",
                Id = new Guid("10000000-0000-0000-0000-000000000000"),
                Value = "app"
            },
            new ConsumerKey{
                Channel = "Web",
                Id = new Guid("20000000-0000-0000-0000-000000000000"),
                Value = "web"
            }
        };
        public async Task<ConsumerKey> Find(string consumerKey)
        {
            return consumerKeys.Find(x => x.Channel.ToLower().Equals(consumerKey.ToLower()));
        }

        public async Task<IEnumerable<ConsumerKey>> FindAll()
        {

            return consumerKeys;
        }
    }
}