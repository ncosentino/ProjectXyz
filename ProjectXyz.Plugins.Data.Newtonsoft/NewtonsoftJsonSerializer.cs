using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Newtonsoft.Json;

using ProjectXyz.Api.Data.Serialization;
using ProjectXyz.Plugins.Data.Newtonsoft.Api;

namespace ProjectXyz.Plugins.Data.Newtonsoft
{
    public sealed class NewtonsoftJsonSerializer : INewtonsoftJsonSerializerFacade
    {
        private readonly JsonSerializer _jsonSerializer;
        private readonly Dictionary<Type, ConvertToSerializableDelegate> _mapping;
        
        private ConvertToSerializableDelegate _defaultConverter;

        public NewtonsoftJsonSerializer(JsonSerializer jsonSerializer)
        {
            _jsonSerializer = jsonSerializer;
            _mapping = new Dictionary<Type, ConvertToSerializableDelegate>();
        }

        public void RegisterDefaultSerializableConverter(ConvertToSerializableDelegate converter)
        {
            _defaultConverter = converter;
        }

        public void Serialize<TSerializable>(
            Stream stream,
            TSerializable objectToSerialize,
            Encoding encoding)
        {
            if (NeedsSerialization(objectToSerialize) &&
                !(objectToSerialize is ISerializable))
            {
                var serializable = GetAsSerializable(
                    objectToSerialize,
                    new HashSet<object>());
                Serialize(stream, serializable, encoding);
                return;
            }

            using (var writer = new JsonTextWriter(new StreamWriter(stream, encoding, 4096, true)))
            {
                _jsonSerializer.Serialize(writer, objectToSerialize);
            }
        }

        public ISerializable GetAsSerializable(
            object objectToSerialize,
            HashSet<object> visited) =>
            GetAsSerializable(
                objectToSerialize,
                objectToSerialize.GetType(),
                visited);

        public ISerializable GetAsSerializable(
            object objectToSerialize,
            Type objectType,
            HashSet<object> visited)
        {
            if (objectToSerialize is ISerializable)
            {
                return (ISerializable)objectToSerialize;
            }

            if (NeedsSerialization(objectType))
            {
                if (!visited.Contains(objectToSerialize))
                {
                    visited.Add(objectToSerialize);
                }
                else
                {
                    // FIXME: is this an issue?
                }
            }

            if (!_mapping.TryGetValue(
                objectType,
                out var converter))
            {
                converter = _defaultConverter;
            }

            if (converter == null)
            {
                throw new InvalidOperationException(
                    $"There is no conversion between '{objectType}' " +
                    $"and '{typeof(ISerializable)}'.");
            }

            var serializable = converter.Invoke(
                this,
                objectToSerialize,
                visited,
                objectType);
            return serializable;
        }

        public object GetObjectToSerialize(
            object obj,
            HashSet<object> visited)
        {
            if (obj == null)
            {
                return null;
            }

            var result = !NeedsSerialization(obj)
                ? obj
                : GetAsSerializable(
                    obj,
                    visited);
            return result;
        }

        public bool NeedsSerialization(object obj) =>
            obj != null &&
            NeedsSerialization(obj.GetType());

        public bool NeedsSerialization(Type type)
        {
            return !(type == typeof(string) || type.IsValueType);
        }

        public void Register(
            Type type,
            ConvertToSerializableDelegate converter)
        {
            _mapping[type] = converter;
        }
    }
}
