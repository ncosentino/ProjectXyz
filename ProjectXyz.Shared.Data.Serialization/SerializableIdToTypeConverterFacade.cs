using System;
using System.Collections.Generic;

using ProjectXyz.Api.Data.Serialization;

namespace ProjectXyz.Shared.Data.Serialization
{
    public sealed class SerializableIdToTypeConverterFacade : ISerializableIdToTypeConverterFacade
    {
        private readonly Dictionary<string, ISerializableIdToTypeConverter> _converters;

        public SerializableIdToTypeConverterFacade()
        {
            _converters = new Dictionary<string, ISerializableIdToTypeConverter>(StringComparer.OrdinalIgnoreCase);
        }

        public Type ConvertToType(string serializableId)
        {
            if (_converters.TryGetValue(
               serializableId,
               out var converter))
            {
                var converterResult = converter.ConvertToType(serializableId);
                return converterResult;
            }

            var defaultConverterResult = Type.GetType(serializableId);
            return defaultConverterResult;
        }

        public void Register(string serializableId, ISerializableIdToTypeConverter converter)
        {
            _converters[serializableId] = converter;
        }
    }
}
