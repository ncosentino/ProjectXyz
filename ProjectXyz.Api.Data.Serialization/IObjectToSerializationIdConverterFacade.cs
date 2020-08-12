using System;

namespace ProjectXyz.Api.Data.Serialization
{
    public interface IObjectToSerializationIdConverterFacade : IObjectToSerializationIdConverter
    {
        void Register(Type type, IObjectToSerializationIdConverter converter);
    }
}
