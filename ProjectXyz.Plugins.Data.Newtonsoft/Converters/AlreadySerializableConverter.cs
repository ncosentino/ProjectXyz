using System;
using System.Collections.Generic;

using ProjectXyz.Api.Data.Serialization;
using ProjectXyz.Plugins.Data.Newtonsoft.Api;

namespace ProjectXyz.Plugins.Data.Newtonsoft
{
    public sealed class AlreadySerializableConverter : IDiscoverableSerializableConverter
    {
        public bool CanConvert(
            INewtonsoftJsonSerializer serializer,
            object objectToConvert,
            Type type,
            string serializableId) => !serializer.NeedsSerialization(type);

        public ISerializable Convert(
            INewtonsoftJsonSerializer serializer,
            object objectToConvert,
            HashSet<object> visited,
            Type type,
            string serializableId)
        {
            return new Serializable(
                serializableId,
                objectToConvert);
        }
    }
}
