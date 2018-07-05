using System;

namespace ProjectXyz.Api.Data.Serialization
{
    public interface IDiscoverableSerializableConverter : ISerializableConverter
    {
        Type Type { get; }

        Type DtoType { get; }
    }
}