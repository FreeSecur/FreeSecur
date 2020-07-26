using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FreeSecur.Core.Serialization
{
    internal static class FsSerialization
    {
        internal static JsonSerializerOptions GetDefaultJsonSerializerOptions()
            => new JsonSerializerOptions(JsonSerializerDefaults.Web);
    }
}
