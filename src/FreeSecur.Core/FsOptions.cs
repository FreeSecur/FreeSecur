using System.Text.Json;

namespace FreeSecur.Core
{
    public class FsOptions
    {
        public FsOptions(
            JsonSerializerOptions jsonSerializerOptions)
        {
            JsonSerializerOptions = jsonSerializerOptions;
        }

        public JsonSerializerOptions JsonSerializerOptions { get; set; }
    }
}
