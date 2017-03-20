using Newtonsoft.Json;
using System;
using xAPI.Client.Resources;

namespace xAPI.Client.Json
{
    internal class XApiVersionConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(XApiVersion);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }
            else if (reader.TokenType == JsonToken.String)
            {
                try
                {
                    return XApiVersion.Parse((string)reader.Value);
                }
                catch (Exception ex)
                {
                    throw new JsonSerializationException($"Error parsing version string: {reader.Value}", ex);
                }
            }
            else
            {
                throw new JsonSerializationException($"Unexpected token or value when parsing version. Token: {reader.TokenType}, Value: {reader.Value}");
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else if (value is XApiVersion)
            {
                writer.WriteValue(value.ToString());
            }
            else
            {
                throw new JsonSerializationException("Expected XApiVersion object value");
            }
        }
    }
}
