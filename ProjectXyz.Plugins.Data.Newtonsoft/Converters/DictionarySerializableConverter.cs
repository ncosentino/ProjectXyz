using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Data.Serialization;
using ProjectXyz.Plugins.Data.Newtonsoft.Api;

namespace ProjectXyz.Plugins.Data.Newtonsoft
{
    public sealed class DictionarySerializableConverter : IDiscoverableSerializableConverter
    {
        public bool CanConvert(
            INewtonsoftJsonSerializer serializer,
            object objectToConvert,
            Type type,
            string serializableId) => typeof(IDictionary).IsAssignableFrom(type);

        public ISerializable Convert(
            INewtonsoftJsonSerializer serializer,
            object objectToConvert,
            HashSet<object> visited,
            Type type,
            string serializableId) => new Serializable(
                serializableId,
                ((IDictionary)objectToConvert)
                .Keys
                .Cast<object>()
                .Select(x => new
                {
                    Key = x,
                    Value = serializer.GetObjectToSerialize(
                        ((IDictionary)objectToConvert)[x],
                        visited)
                })
                .ToDictionary(x => x.Key, x => x.Value));
    }
}
