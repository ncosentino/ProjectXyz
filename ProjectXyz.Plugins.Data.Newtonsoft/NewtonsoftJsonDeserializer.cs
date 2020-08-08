using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using ProjectXyz.Api.Data.Serialization;
using ProjectXyz.Plugins.Data.Newtonsoft.Api;

namespace ProjectXyz.Plugins.Data.Newtonsoft
{
    public sealed class NewtonsoftJsonDeserializer : INewtonsoftJsonDeserializerFacade
    {
        private readonly JsonSerializer _jsonSerializer;
        private readonly Dictionary<string, NewtonsoftDeserializeDelegate> _mapping;

        private NewtonsoftDeserializeDelegate _defaultConverter;

        public NewtonsoftJsonDeserializer(JsonSerializer jsonSerializer)
        {
            _jsonSerializer = jsonSerializer;
            _mapping = new Dictionary<string, NewtonsoftDeserializeDelegate>();
        }

        public void RegisterDefaultDeserializableConverter(NewtonsoftDeserializeDelegate converter)
        {
            _defaultConverter = converter;
        }

        public void Register(
            string serializableId,
            NewtonsoftDeserializeDelegate converter)
        {
            _mapping[serializableId] = converter;
        }

        public JObject ReadObject(Stream stream)
        {
            JObject jsonObject;
            using (var reader = new JsonTextReader(new StreamReader(stream, Encoding.UTF8, false, 4096, true)))
            {
                jsonObject = _jsonSerializer.Deserialize<JObject>(reader);
            }

            return jsonObject;
        }

        public TDeserializable Deserialize<TDeserializable>(Stream stream)
        {
            var jsonObject = ReadObject(stream);
            var deserialized = Deserialize<TDeserializable>(jsonObject);
            return deserialized;
        }

        public TDeserializable Deserialize<TDeserializable>(JObject serializable)
        {
            var objectData = serializable[nameof(ISerializable.Data)];
            if (objectData.Type == JTokenType.Array)
            {
                var arrayData = objectData.Value<JArray>();
                var deserializedList = new List<object>();
                foreach (var child in arrayData.Children().Cast<JObject>())
                {
                    var deserializedChild = Deserialize<object>(child);
                    deserializedList.Add(deserializedChild);
                }

                return (TDeserializable)(object)deserializedList;
            }

            var rawObjectData = objectData
                .Value<JObject>()
                .ToString(Formatting.None);

            var serializableId = serializable[nameof(ISerializable.SerializableId)].Value<string>();
            var type = Type.GetType(serializableId);

            if (!_mapping.TryGetValue(
                serializableId,
                out var converter))
            {
                converter = _defaultConverter;
            }

            if (converter != null)
            {
                var nestedStream = new MemoryStream(Encoding.UTF8.GetBytes(rawObjectData));
                var converted = converter.Invoke(
                    this,
                    nestedStream,
                    type ?? typeof(TDeserializable));
                return (TDeserializable)converted;
            }

            if (type == null)
            {
                throw new NotSupportedException(
                    $"There is no mechanism to deserialize '{serializableId}' " +
                    $"and no type could be found that matches this serialization " +
                    $"id.");
            }

            var deserialized = JsonConvert.DeserializeObject(
                rawObjectData,
                type);
            var casted = (TDeserializable)deserialized;
            return casted;
        }
    }
}
