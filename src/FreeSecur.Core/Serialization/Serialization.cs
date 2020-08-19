using System.Text.Json;
using System.Text.Json.Serialization;

namespace FreeSecur.Core.Serialization
{
    public static class Serialization
    {
        public static JsonSerializerOptions GetDefaultJsonSerializerOptions()
        {
            var jsonSerializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);

            ConfigureSerializerOptions(jsonSerializerOptions);

            return jsonSerializerOptions;
        }

        public static void ConfigureSerializerOptions(JsonSerializerOptions options)
        {
            options.Converters.Add(new JsonStringEnumConverter());
        }
    }
}
