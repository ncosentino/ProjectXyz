using System;

namespace ProjectXyz.Api.Data.Serialization
{
    public interface ISerializableConverterRegistrar
    {
        void Register(
            Type type,
            Type dtoType,
            ISerializableConverter converter);
    }
}