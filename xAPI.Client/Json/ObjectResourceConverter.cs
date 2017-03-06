using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using xAPI.Client.Resources;

namespace xAPI.Client.Json
{
    public class ObjectResourceConverter : JsonConverter
    {
        public override bool CanWrite { get { return false; } }

        public override bool CanConvert(Type objectType)
        {
            return typeof(ObjectResource).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }
            else if (reader.TokenType == JsonToken.StartObject)
            {
                JObject obj = JObject.Load(reader);
                string jsonObjectType = (string)obj["objectType"];
                ObjectResource target = this.CreateEmptyObject(objectType, jsonObjectType);
                serializer.Populate(obj.CreateReader(), target);
                return target;
            }
            else
            {
                throw new JsonSerializationException($"Unexpected token or value when parsing ObjectResource. Token: {reader.TokenType}, Value: {reader.Value}");
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        private ObjectResource CreateEmptyObject(Type objectType, string jsonObjectType)
        {
            if (!objectType.IsAbstract)
            {
                return (ObjectResource)Activator.CreateInstance(objectType);
            }
            else
            {
                if (jsonObjectType == "Agent")
                {
                    return new Agent();
                }
                else if (jsonObjectType == "Person")
                {
                    return new Person();
                }
                else if (jsonObjectType == "Activity" || jsonObjectType == null)
                {
                    return new Activity();
                }
                else
                {
                    throw new JsonSerializationException($"Error when parsing ObjectResource. Invalid objectType detected: {jsonObjectType}.");
                }
            }
        }
    }
}
