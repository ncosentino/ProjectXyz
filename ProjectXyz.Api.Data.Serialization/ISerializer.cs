using System.IO;
using System.Text;

namespace ProjectXyz.Api.Data.Serialization
{
    public interface ISerializer
    {
        void Serialize<TSerializable>(
            Stream stream,
            TSerializable serializable,
            Encoding encoding);
    }
}