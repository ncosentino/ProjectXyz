using System;
using System.Collections.Generic;

using ProjectXyz.Api.Data.Serialization;

namespace ProjectXyz.Plugins.Data.Newtonsoft.Api
{
    public delegate ISerializable ConvertToSerializableDelegate(
        INewtonsoftJsonSerializer serializer,
        object objectToConvert,
        HashSet<object> visited,
        Type type,
        string serializableId);

    public interface INewtonsoftJsonSerializer : ISerializer
    {
        bool NeedsSerialization(object obj);

        bool NeedsSerialization(Type type);

        ISerializable GetAsSerializable(
            object objectToSerialize,
            HashSet<object> visited);

        ISerializable GetAsSerializable(
            object objectToSerialize,
            Type objectType,
            HashSet<object> visited);

        object GetObjectToSerialize(
            object obj,
            HashSet<object> visited);
    }
}
