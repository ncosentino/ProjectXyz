using System;

namespace ProjectXyz.Shared.Game.GameObjects.Generation.Data.Json
{
    public interface IGeneratorAttributeValueSerializerRegistrar
    {
        void Register(
            Type type,
            IGeneratorAttributeValueSerializer generatorAttributeValueSerializer);
    }
}