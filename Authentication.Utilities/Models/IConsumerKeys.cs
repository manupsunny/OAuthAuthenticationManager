using System.Collections.Generic;

namespace Authentication.Models.Models
{
    public interface IConsumerKeys
    {
        IEnumerable<ConsumerKey> GetAll();
    }
}