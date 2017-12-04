using System.Collections.Generic;
using System.Threading.Tasks;
using Authentication.Model.Models;

namespace Authentication.Model.Repositories
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