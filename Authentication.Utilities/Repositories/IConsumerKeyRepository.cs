using System.Collections.Generic;
using System.Threading.Tasks;
using Authentication.Utilities.Models;

namespace Authentication.Utilities.Repositories
{
    public interface IConsumerKeyRepository
    {
        Task<IEnumerable<ConsumerKey>> FindAll();
        Task<ConsumerKey> Find(string consumerkeyValue);
    }
}