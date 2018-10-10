using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace xAPI.Client.Json
{
    internal class StrictJsonMediaTypeFormatter : MediaTypeFormatter
    {
        public JsonSerializerSettings SerializerSettings { get; private set; }

        public StrictJsonMediaTypeFormatter()
        {
            this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));
            this.SupportedEncodings.Add(new UTF8Encoding(encoderShouldEmitUTF8Identifier: false, throwOnInvalidBytes: true));
            this.SerializerSettings = new JsonSerializerSettings()
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                TypeNameHandling = TypeNameHandling.None
            };
        }

        public override bool CanReadType(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return true;
        }

        public override bool CanWriteType(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return true;
        }

        public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (readStream == null)
            {
                throw new ArgumentNullException(nameof(readStream));
            }

            // If content length is 0 then return default value for this type
            if (content?.Headers?.ContentLength == 0)
            {
                object result = GetDefaultValueForType(type);
                return Task.FromResult(result);
            }

            // Get the character encoding for the content
            Encoding effectiveEncoding = this.SelectCharacterEncoding(content?.Headers);

            // Deserialize
            using (var jsonTextReader = new JsonTextReader(new StreamReader(readStream, effectiveEncoding)) { CloseInput = false })
            {
                var serializer = JsonSerializer.Create(this.SerializerSettings);
                object result = serializer.Deserialize(jsonTextReader, type);
                return Task.FromResult(result);
            }
        }

        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (writeStream == null)
            {
                throw new ArgumentNullException(nameof(writeStream));
            }

            // Get the character encoding for the content
            Encoding effectiveEncoding = this.SelectCharacterEncoding(content?.Headers);

            // Serialize
            using (var jsonTextWriter = new JsonTextWriter(new StreamWriter(writeStream, effectiveEncoding)) { CloseOutput = false, Formatting = Formatting.Indented })
            {
                var serializer = JsonSerializer.Create(this.SerializerSettings);
                serializer.Serialize(jsonTextWriter, value);
                jsonTextWriter.Flush();
            }

            return Task.FromResult(0);
        }
    }
}
