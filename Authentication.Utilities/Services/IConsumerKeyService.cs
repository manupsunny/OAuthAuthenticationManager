using System.Collections.Generic;
using System.Threading.Tasks;
using Authentication.Models.Models;

namespace Authentication.Models.Services
{
    public interface IConsumerKeyService
    {
        Task<IEnumerable<ConsumerKey>> GetAllConsumerKeysAsync();
        IEnumerable<ConsumerKey> GetAllConsumerKeysSync();
        Task<string> GetConsumerChannel(string consumerKey);
    }
}