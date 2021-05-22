using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using NexusLabs.Framework;

using ProjectXyz.Api.Data.Serialization;
using ProjectXyz.Plugins.Data.Newtonsoft.Api;

namespace ProjectXyz.Plugins.Data.Newtonsoft
{
    public sealed class NewtonsoftJsonDeserializer : INewtonsoftJsonDeserializerFacade
    {
        private readonly JsonSerializer _jsonSerializer;
        private readonly ICast _cast;
        private readonly Dictionary<string, NewtonsoftDeserializeDelegate> _mapping;

        private NewtonsoftDeserializeDelegate _defaultConverter;

        public NewtonsoftJsonDeserializer(
            JsonSerializer jsonSerializer,
            ICast cast)
        {
            _jsonSerializer = jsonSerializer;
            _cast = cast;
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

        public object ReadObject(Stream stream)
        {
            object jsonObject;
            using (var reader = new JsonTextReader(new StreamReader(stream, Encoding.UTF8, false, 4096, true)))
            {
                jsonObject = _jsonSerializer.Deserialize<object>(reader);
            }

            return jsonObject;
        }

        public TDeserializable Deserialize<TDeserializable>(Stream stream)
        {
            var jsonObject = ReadObject(stream);
            if (jsonObject is JObject)
            {
                var deserialized = Deserialize<TDeserializable>((JObject)jsonObject);
                return deserialized;
            }

            return (TDeserializable)jsonObject;
        }

        public TDeserializable Deserialize<TDeserializable>(JObject serializable)
        {
            var objectData = serializable[nameof(ISerializable.Data)];
            if (!NeedsDeserialization(objectData.Type))
            {
                return (TDeserializable)objectData.Value<JValue>().Value;
            }

            var serializableId = serializable[nameof(ISerializable.SerializableId)].Value<string>();

            if (!_mapping.TryGetValue(
                serializableId,
                out var converter))
            {
                converter = _defaultConverter;

                if (objectData.Type == JTokenType.Array &&
                    serializableId.IndexOf("Dictionary", StringComparison.OrdinalIgnoreCase) == -1)
                {
                    var arrayData = objectData.Value<JArray>();
                    var deserializedList = new List<object>();
                    foreach (var child in arrayData.Children())
                    {
                        if (child is JObject)
                        {
                            var deserializedChild = Deserialize<object>((JObject)child);
                            deserializedList.Add(deserializedChild);
                        }
                        else if (child is JValue)
                        {
                            var value = ((JValue)child).Value;

                            // FIXME: this is a hack but newtonsoft is hell-bent
                            // on making ints longs and while i applaud them for it,
                            // it becomes a nightmare if you're essentially never
                            // using longs!
                            if (value is long)
                            {
                                value = Convert.ToInt32(value, CultureInfo.InvariantCulture);
                            }

                            deserializedList.Add(value);
                        }
                        else
                        {
                            throw new NotSupportedException(
                                $"Child JSON entry '{child}' is not supported.");
                        }
                    }

                    return _cast.ToType<TDeserializable>(deserializedList);
                }
            }

            if (converter != null)
            {
                var rawObjectData = objectData.Value<object>();
                var stringObjectData = ((JToken)rawObjectData).ToString(Formatting.None);
                using (var nestedStream = new MemoryStream(Encoding.UTF8.GetBytes(stringObjectData)))
                {
                    var converted = converter.Invoke(
                        this,
                        nestedStream,
                        serializableId);
                    return (TDeserializable)converted;
                }
            }

            throw new NotSupportedException(
                $"There is no mechanism to deserialize '{serializableId}' " +
                $"and no type could be found that matches this serialization " +
                $"id.");
        }

        public bool NeedsDeserialization(JTokenType type)
        {
            return
                type != JTokenType.String && 
                type != JTokenType.Boolean &&
                type != JTokenType.Float &&
                type != JTokenType.Integer;
        }
    }
}
