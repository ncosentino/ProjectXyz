using System;
using System.Collections.Generic;

using ProjectXyz.Api.Data.Serialization;

namespace ProjectXyz.Plugins.Data.Newtonsoft.Api
{
    public delegate ISerializable ConvertToSerializableDelegate(
        INewtonsoftJsonSerializer serializer,
        object objectToConvert,
        HashSet<object> visited,
        Type type);

    public interface INewtonsoftJsonSerializer : ISerializer
    {
        bool NeedsSerialization(object obj);

        bool NeedsSerialization(Type type);

        ISerializable GetAsSerializable(
            object objectToSerialize,
            HashSet<object> visited);
    }
}
