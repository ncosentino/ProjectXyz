using Newtonsoft.Json;

namespace ProjectXyz.Shared.Game.GameObjects.Generation.Data.Json
{
    [JsonConverter(typeof(SerializableConverter))]
    public interface ISerializableDto
    {
        string SerializableId { get; }

        ISerializableDtoData Data { get; }
    }
}