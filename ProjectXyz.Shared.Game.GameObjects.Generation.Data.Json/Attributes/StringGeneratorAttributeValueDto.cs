using ProjectXyz.Api.Data.Serialization;

namespace ProjectXyz.Shared.Game.GameObjects.Generation.Data.Json.Attributes
{
    public sealed class StringSerializableDtoValue : ISerializableDtoData
    {
        public string Value { get; set; }
    }
}