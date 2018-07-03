using System.IO;

namespace ProjectXyz.Shared.Game.GameObjects.Generation.Data.Json
{
    public interface IDeserializer
    {
        TDeserializable Deserialize<TDeserializable>(Stream stream);
    }
}