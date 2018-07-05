using System.IO;

namespace ProjectXyz.Api.Data.Serialization
{
    public interface IDeserializer
    {
        TDeserializable Deserialize<TDeserializable>(Stream stream);
    }
}