using System;
using System.Collections.Generic;

using ProjectXyz.Api.Data.Serialization;

namespace ProjectXyz.Plugins.Data.Newtonsoft.Api
{
    public interface ISerializableConverter
    {
        ISerializable Convert(
            INewtonsoftJsonSerializer serializer,
            object objectToConvert,
            HashSet<object> visited,
            Type type,
            string serializableId);
    }
}
