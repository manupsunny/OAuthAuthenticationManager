using System.Collections.Generic;
using System.Threading.Tasks;
using Authentication.Model.Models;

namespace Authentication.Model.Services
{
    public interface IConsumerKeyService
    {
        Task<IEnumerable<ConsumerKey>> GetAllConsumerKeysAsync();
        IEnumerable<ConsumerKey> GetAllConsumerKeysSync();
        Task<string> GetConsumerChannel(string consumerKey);
    }
}