using System;
using System.Collections.Generic;

using ProjectXyz.Api.Data.Serialization;

namespace ProjectXyz.Shared.Data.Serialization
{
    public sealed class ObjectToSerializationIdConverterFacade : IObjectToSerializationIdConverterFacade
    {
        private readonly Dictionary<Type, IObjectToSerializationIdConverter> _converters;

        public ObjectToSerializationIdConverterFacade()
        {
            _converters = new Dictionary<Type, IObjectToSerializationIdConverter>();
        }

        public string ConvertToSerializationId(object obj) =>
            ConvertToSerializationId(obj.GetType());

        public string ConvertToSerializationId(Type type)
        {
            if (_converters.TryGetValue(
                type,
                out var converter))
            {
                var converterResult = converter.ConvertToSerializationId(type);
                return converterResult;
            }

            var defaultConverterResult = type.AssemblyQualifiedName;
            return defaultConverterResult;
        }

        public void Register(Type type, IObjectToSerializationIdConverter converter)
        {
            _converters[type] = converter;
        }
    }
}
