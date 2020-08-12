using System;
using System.Collections.Generic;

namespace ProjectXyz.Api.Data.Serialization
{
    public interface IDiscoverableTypedObjectToSerializationIdConverter : IObjectToSerializationIdConverter
    {
        IEnumerable<Type> ConvertableTypes { get; }
    }
}
