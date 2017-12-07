using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Authentication.Model.Services
{
    public sealed class CustomJSONSerializer : JsonSerializer
    {
        public CustomJSONSerializer()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver();
            Converters.Add(new StringEnumConverter
            {
                AllowIntegerValues = false,
                CamelCaseText = false
            });
            Formatting = Formatting.Indented;
            NullValueHandling = NullValueHandling.Ignore;
        }
    }
}