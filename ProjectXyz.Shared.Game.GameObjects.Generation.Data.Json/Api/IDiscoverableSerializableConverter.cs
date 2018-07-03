using System;

namespace ProjectXyz.Shared.Game.GameObjects.Generation.Data.Json
{
    public interface IDiscoverableSerializableConverter : ISerializableConverter
    {
        Type DtoType { get; }
    }
}