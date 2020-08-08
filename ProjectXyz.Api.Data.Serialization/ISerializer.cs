using System.IO;

namespace ProjectXyz.Api.Data.Serialization
{
    public interface ISerializer
    {
        void Serialize<TSerializable>(Stream stream, TSerializable serializable);
    }
}