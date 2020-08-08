using System.IO;

namespace ProjectXyz.Api.Data.Serialization
{
    public static class IDeserializerExtensions
    {
        public static object Deserialize(
            this IDeserializer deserializer,
            Stream stream)
        {
            var result = deserializer.Deserialize<object>(stream);
            return result;
        }
    }
}
