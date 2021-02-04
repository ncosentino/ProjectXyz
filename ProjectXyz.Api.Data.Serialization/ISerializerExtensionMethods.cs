using System.IO;
using System.Text;

namespace ProjectXyz.Api.Data.Serialization
{
    public static class ISerializerExtensionMethods
    {
        public static string SerializeToString<TSerializable>(
            this ISerializer serializer,
            TSerializable serializable,
            Encoding encoding)
        {
            using (var outStream = new MemoryStream())
            {
                serializer.Serialize(outStream, serializable, encoding);
                outStream.Position = 0;

                using (var streamReader = new StreamReader(outStream, encoding))
                {
                    var serialized = streamReader.ReadToEnd();
                    return serialized;
                }
            }
        }
    }
}