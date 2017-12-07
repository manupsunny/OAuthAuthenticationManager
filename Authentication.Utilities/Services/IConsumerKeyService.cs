using System.Collections.Generic;
using System.Threading.Tasks;
using Authentication.Utilities.Models;

namespace Authentication.Utilities.Services
{
    public interface IConsumerKeyService
    {
        Task<IEnumerable<ConsumerKey>> GetAllConsumerKeysAsync();
        IEnumerable<ConsumerKey> GetAllConsumerKeysSync();
        Task<string> GetConsumerChannel(string consumerKey);
    }
}