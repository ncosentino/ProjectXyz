using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Data.Serialization;
using ProjectXyz.Plugins.Data.Newtonsoft.Api;

namespace ProjectXyz.Plugins.Data.Newtonsoft
{
    public sealed class EnumerableSerializableConverter : IDiscoverableSerializableConverter
    {
        public bool CanConvert(
            INewtonsoftJsonSerializer serializer,
            object objectToConvert,
            Type type,
            string serializableId) =>
                typeof(IEnumerable).IsAssignableFrom(type) &&
                !typeof(IDictionary).IsAssignableFrom(type);

        public ISerializable Convert(
            INewtonsoftJsonSerializer serializer,
            object objectToConvert,
            HashSet<object> visited,
            Type type,
            string serializableId) => new Serializable(
                serializableId,
                ((IEnumerable)objectToConvert)
                    .Cast<object>()
                    .Select(x => serializer.GetObjectToSerialize(
                        x,
                        visited))
                    .Where(x => x != null));
    }
}
