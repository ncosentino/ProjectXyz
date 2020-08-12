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

        public string ConvertToSerializationId(object obj)
        {
            if (_converters.TryGetValue(
                obj.GetType(),
                out var converter))
            {
                var converterResult = converter.ConvertToSerializationId(obj);
                return converterResult;
            }

            var defaultConverterResult = obj
                .GetType()
                .AssemblyQualifiedName;
            return defaultConverterResult;
        }

        public void Register(Type type, IObjectToSerializationIdConverter converter)
        {
            _converters[type] = converter;
        }
    }
}
