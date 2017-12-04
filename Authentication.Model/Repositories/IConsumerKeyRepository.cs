using System.Collections.Generic;
using System.Threading.Tasks;
using Authentication.Model.Models;

namespace Authentication.Model.Repositories
{
    public interface IConsumerKeyRepository
    {
        Task<IEnumerable<ConsumerKey>> FindAll();
        Task<ConsumerKey> Find(string consumerkeyValue);
    }
}