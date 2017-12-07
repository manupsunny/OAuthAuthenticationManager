using System.Collections.Generic;
using System.Linq;
using Authentication.Utilities.Models;
using Nancy;

namespace Authentication.Utilities.Validators
{
    public static class ConsumerKeyValidator
    {
        public static bool Validate(NancyContext context, IEnumerable<ConsumerKey> validConsumerKeys)
        {
            var consumerKeyValue = context.Request.Headers["Consumer-Key"].FirstOrDefault();
            var consumerKey = validConsumerKeys.FirstOrDefault(c => c.Value.Equals(consumerKeyValue));

            if (string.IsNullOrWhiteSpace(consumerKeyValue) || consumerKey == null) return false;
            context.Items.Add("ConsumerKey", consumerKey);
            return true;
        }
    }
}