using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Data.Serialization;
using ProjectXyz.Plugins.Data.Newtonsoft.Api;

namespace ProjectXyz.Plugins.Data.Newtonsoft
{
    public sealed class SerializableConverterFacade : ISerializableConverterFacade
    {
        private readonly IReadOnlyCollection<IDiscoverableSerializableConverter> _converters;

        public SerializableConverterFacade(IEnumerable<IDiscoverableSerializableConverter> converters)
        {
            _converters = converters.ToArray();
        }

        public ISerializable Convert(
            INewtonsoftJsonSerializer serializer,
            object objectToConvert,
            HashSet<object> visited,
            Type type,
            string serializableId)
        {
            var converter = _converters
                .FirstOrDefault(x => x.CanConvert(
                    serializer,
                    objectToConvert,
                    type,
                    serializableId));
            if (converter == null)
            {
                throw new NotSupportedException(
                    $"There is no converter that can handle converting " +
                    $"'{objectToConvert}'.");
            }

            var converted = converter.Convert(
                serializer,
                objectToConvert,
                visited,
                type,
                serializableId);
            return converted;
        }
    }
}
