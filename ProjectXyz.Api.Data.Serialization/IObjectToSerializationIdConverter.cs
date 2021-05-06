using System;

namespace ProjectXyz.Api.Data.Serialization
{
    public interface IObjectToSerializationIdConverter
    {
        string ConvertToSerializationId(object obj);

        string ConvertToSerializationId(Type type);
    }
}
