using System;

namespace ProjectXyz.Api.Data.Serialization
{
    public interface ISerializableIdToTypeConverter
    {
        Type ConvertToType(string serializableId);
    }
}
