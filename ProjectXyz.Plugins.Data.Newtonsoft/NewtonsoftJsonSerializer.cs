﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Newtonsoft.Json;

using ProjectXyz.Api.Data.Serialization;
using ProjectXyz.Plugins.Data.Newtonsoft.Api;

namespace ProjectXyz.Plugins.Data.Newtonsoft
{
    public sealed class NewtonsoftJsonSerializer : INewtonsoftJsonSerializerFacade
    {
        private readonly JsonSerializer _jsonSerializer;
        private readonly IObjectToSerializationIdConverterFacade _objectToSerializationIdConverterFacade;
        private readonly Dictionary<Type, ConvertToSerializableDelegate> _mapping;
        private readonly HashSet<Type> _ignoredTypes;
        private readonly HashSet<Type> _allowedTypes;

        private ConvertToSerializableDelegate _defaultConverter;

        public NewtonsoftJsonSerializer(
            JsonSerializer jsonSerializer,
            IObjectToSerializationIdConverterFacade objectToSerializationIdConverterFacade)
        {
            _jsonSerializer = jsonSerializer;
            _objectToSerializationIdConverterFacade = objectToSerializationIdConverterFacade;
            _mapping = new Dictionary<Type, ConvertToSerializableDelegate>();
            _ignoredTypes = new HashSet<Type>();
            _allowedTypes = new HashSet<Type>();
        }

        public void RegisterDefaultSerializableConverter(ConvertToSerializableDelegate converter)
        {
            _defaultConverter = converter;
        }

        public void RegisterTypeToIgnore(Type type)
        {
            _ignoredTypes.Add(type);
        }

        public void RegisterAllowedType(Type type)
        {
            _allowedTypes.Add(type);
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
            if (_ignoredTypes.Contains(objectType))
            {
                return null;
            }

            if (_allowedTypes.Any() && !_allowedTypes.Contains(objectType))
            {
                return null;
            }

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

            var serializableId = _objectToSerializationIdConverterFacade.ConvertToSerializationId(objectToSerialize);
            var serializable = converter.Invoke(
                this,
                objectToSerialize,
                visited,
                objectType,
                serializableId);
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
