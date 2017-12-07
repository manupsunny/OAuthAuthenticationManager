using System.Collections.Generic;

namespace Authentication.Utilities.Models
{
    public interface IConsumerKeys
    {
        IEnumerable<ConsumerKey> GetAll();
    }
}