using System.Collections.Generic;
using System.Threading.Tasks;
using Authentication.Models.Models;

namespace Authentication.Models.Repositories
{
    public class ConsumerKeyRepository : IConsumerKeyRepository
    {
        public async Task<ConsumerKey> Find(string consumerKey)
        {
            return null;
        }

        public async Task<IEnumerable<ConsumerKey>> FindAll()
        {
            return null;
        }
    }
}