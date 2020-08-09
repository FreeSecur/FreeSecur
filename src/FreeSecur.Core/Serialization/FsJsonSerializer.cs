﻿using System.Text.Json;

namespace FreeSecur.Core
{
    public class FsJsonSerializer : IFsSerializer
    {
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public FsJsonSerializer(JsonSerializerOptions jsonSerializerOptions)
        {
            _jsonSerializerOptions = jsonSerializerOptions;
        }

        public string Serialize<T>(T value)
            => JsonSerializer.Serialize(value, _jsonSerializerOptions);

        public T Deserialize<T>(string json)
            => JsonSerializer.Deserialize<T>(json, _jsonSerializerOptions);
    }
}
