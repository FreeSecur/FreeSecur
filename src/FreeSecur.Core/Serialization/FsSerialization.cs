using System.Text.Json;
using System.Text.Json.Serialization;

namespace FreeSecur.Core.Serialization
{
    public static class FsSerialization
    {
        public static JsonSerializerOptions GetDefaultJsonSerializerOptions()
        {
            var jsonSerializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);

            ConfigureSerailizerOptions(jsonSerializerOptions);

            return jsonSerializerOptions;
        }

        public static void ConfigureSerailizerOptions(JsonSerializerOptions options)
        {
            options.Converters.Add(new JsonStringEnumConverter());
        }
    }
}
