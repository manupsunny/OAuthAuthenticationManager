using System.Collections.Generic;

namespace Authentication.Model.Models
{
    public interface IConsumerKeys
    {
        IEnumerable<ConsumerKey> GetAll();
    }
}