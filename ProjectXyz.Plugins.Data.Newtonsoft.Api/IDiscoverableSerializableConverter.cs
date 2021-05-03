using System;

namespace ProjectXyz.Plugins.Data.Newtonsoft.Api
{
    public interface IDiscoverableSerializableConverter : ISerializableConverter
    {
        bool CanConvert(
            INewtonsoftJsonSerializer serializer,
            object objectToConvert,
            Type type,
            string serializableId);
    }
}
