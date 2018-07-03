using System;

namespace ProjectXyz.Shared.Game.GameObjects.Generation.Data.Json
{
    public interface IDiscoverableGeneratorAttributeValueSerializer : IGeneratorAttributeValueSerializer
    {
        Type SerializableType { get; }
    }
}