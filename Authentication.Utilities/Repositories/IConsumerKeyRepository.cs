using System.Collections.Generic;
using System.Threading.Tasks;
using Authentication.Models.Models;

namespace Authentication.Models.Repositories
{
    public interface IConsumerKeyRepository
    {
        Task<IEnumerable<ConsumerKey>> FindAll();
        Task<ConsumerKey> Find(string consumerkeyValue);
    }
}