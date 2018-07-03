using System.IO;

namespace ProjectXyz.Shared.Game.GameObjects.Generation.Data.Json
{
    public interface IDtoSerializer
    {
        Stream Serialize(ISerializableDto dto);
    }
}