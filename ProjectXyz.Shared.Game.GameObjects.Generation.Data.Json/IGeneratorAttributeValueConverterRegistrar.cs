using System;

namespace ProjectXyz.Shared.Game.GameObjects.Generation.Data.Json
{
    public interface IGeneratorAttributeValueConverterRegistrar
    {
        void Register(
            Type dtoType,
            ISerializableConverter converter);
    }
}