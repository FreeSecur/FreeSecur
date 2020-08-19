using System.Text.Json;

namespace FreeSecur.API.Core
{
    public class Options
    {
        public Options(
            JsonSerializerOptions jsonSerializerOptions)
        {
            JsonSerializerOptions = jsonSerializerOptions;
        }

        public JsonSerializerOptions JsonSerializerOptions { get; set; }
    }
}
