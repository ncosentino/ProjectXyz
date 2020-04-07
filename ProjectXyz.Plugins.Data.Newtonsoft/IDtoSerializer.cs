using System.IO;

namespace ProjectXyz.Plugins.Data.Newtonsoft
{
    public interface IDtoSerializer
    {
        Stream Serialize(ISerializableDto dto);
    }
}