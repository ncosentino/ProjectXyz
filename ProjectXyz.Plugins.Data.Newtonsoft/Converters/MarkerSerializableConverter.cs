using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Data.Serialization;
using ProjectXyz.Plugins.Data.Newtonsoft.Api;

namespace ProjectXyz.Plugins.Data.Newtonsoft
{
    public sealed class MarkerSerializableConverter : IDiscoverableSerializableConverter
    {
        public bool CanConvert(
            INewtonsoftJsonSerializer serializer,
            object objectToConvert,
            Type type,
            string serializableId) =>
                !type.GetConstructors().Any(x =>
                    x.IsPublic &&
                    x.GetParameters().Any()) &&
                type.GetConstructors().Any(x =>
                    x.IsPublic &&
                    !x.GetParameters().Any()) &&
                !type.GetProperties().Any(x => x.CanWrite);

        public ISerializable Convert(
            INewtonsoftJsonSerializer serializer,
            object objectToConvert,
            HashSet<object> visited,
            Type type,
            string serializableId)
        {
            return new Serializable(
                serializableId,
                null);
        }
    }
}
