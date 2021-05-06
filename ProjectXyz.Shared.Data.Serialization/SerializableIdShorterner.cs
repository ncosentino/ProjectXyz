using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Data.Serialization;

namespace ProjectXyz.Shared.Data.Serialization
{
    public sealed class SerializableIdShorterner :
            IDiscoverableTypedObjectToSerializationIdConverter,
            IDiscoverablSerializationIdToTypeConverter
    {
        private readonly Dictionary<Type, string> _typesToIds;
        private readonly Dictionary<string, Type> _idsToTypes;

        public SerializableIdShorterner(IEnumerable<KeyValuePair<Type, string>> typesToIds)
        {
            _typesToIds = typesToIds.ToDictionary(
                x => x.Key,
                x => x.Value);
            _idsToTypes = _typesToIds.ToDictionary(
                x => x.Value,
                x => x.Key,
                StringComparer.OrdinalIgnoreCase);
        }

        public IEnumerable<Type> ConvertableTypes => _typesToIds.Keys;

        public IEnumerable<string> ConvertableSerializableIds => _typesToIds.Values;

        public string ConvertToSerializationId(Type type) => _typesToIds[type];

        public string ConvertToSerializationId(object obj) =>
            ConvertToSerializationId(obj.GetType());

        public Type ConvertToType(string serializableId) => _idsToTypes[serializableId];
    }
}
