using System.Collections.Generic;

namespace ProjectXyz.Api.Data.Serialization
{
    public interface IDiscoverablSerializationIdToTypeConverter : ISerializableIdToTypeConverter
    {
        IEnumerable<string> ConvertableSerializableIds { get; }
    }
}
