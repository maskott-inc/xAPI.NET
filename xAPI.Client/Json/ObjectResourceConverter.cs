using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using xAPI.Client.Resources;

namespace xAPI.Client.Json
{
    public class ObjectResourceConverter<T> : JsonConverter where T : IObjectResource
    {
        public override bool CanWrite { get { return false; } }

        public override bool CanConvert(Type objectType)
        {
            return typeof(IObjectResource).IsAssignableFrom(objectType);
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
                IObjectResource target = this.CreateEmptyObject(objectType, jsonObjectType);
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

        private IObjectResource CreateEmptyObject(Type objectType, string jsonObjectType)
        {
            if (jsonObjectType == "Agent")
            {
                return new Agent();
            }
            else if (jsonObjectType == "Person")
            {
                return new Person();
            }
            else if (jsonObjectType == "Group")
            {
                return new Group();
            }
            else if (jsonObjectType == "StatementRef")
            {
                return new StatementRef();
            }
            else if (jsonObjectType == "SubStatement")
            {
                return new SubStatement();
            }
            else if (jsonObjectType == "Activity")
            {
                return new Activity();
            }
            else if (string.IsNullOrEmpty(jsonObjectType))
            {
                return Activator.CreateInstance<T>();
            }
            else
            {
                throw new JsonSerializationException($"Error when parsing ObjectResource. Invalid objectType detected: {jsonObjectType}.");
            }
        }
    }
}
