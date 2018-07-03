using System.IO;
using ProjectXyz.Api.GameObjects.Generation.Attributes;

namespace ProjectXyz.Shared.Game.GameObjects.Generation.Data.Json
{
    public interface IGeneratorAttributeValueSerializer
    {
        Stream Serialize(IGeneratorAttributeValue generatorAttributeValue);
    }
}