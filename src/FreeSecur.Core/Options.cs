using System.Text.Json;

namespace FreeSecur.Core
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
