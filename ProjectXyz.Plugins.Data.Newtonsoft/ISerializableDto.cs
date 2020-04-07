using Newtonsoft.Json;
using ProjectXyz.Api.Data.Serialization;

namespace ProjectXyz.Plugins.Data.Newtonsoft
{
    [JsonConverter(typeof(SerializableConverter))]
    public interface ISerializableDto
    {
        string SerializableId { get; }

        ISerializableDtoData Data { get; }
    }
}