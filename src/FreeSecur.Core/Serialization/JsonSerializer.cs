using System.Text.Json;

namespace FreeSecur.Core
{
    public class JsonSerializer : ISerializer
    {
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public JsonSerializer(JsonSerializerOptions jsonSerializerOptions)
        {
            _jsonSerializerOptions = jsonSerializerOptions;
        }

        public string Serialize<T>(T value)
            => System.Text.Json.JsonSerializer.Serialize(value, _jsonSerializerOptions);

        public T Deserialize<T>(string json)
            => System.Text.Json.JsonSerializer.Deserialize<T>(json, _jsonSerializerOptions);
    }
}
