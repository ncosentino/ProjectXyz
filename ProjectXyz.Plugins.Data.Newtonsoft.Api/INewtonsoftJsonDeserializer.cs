using System.IO;

using Newtonsoft.Json.Linq;

using ProjectXyz.Api.Data.Serialization;

namespace ProjectXyz.Plugins.Data.Newtonsoft.Api
{
    public delegate object NewtonsoftDeserializeDelegate(
        INewtonsoftJsonDeserializer deserializer,
        Stream stream,
        string serializableId);

    public interface INewtonsoftJsonDeserializer : IDeserializer
    {
        TDeserializable Deserialize<TDeserializable>(JObject serializable);

        object ReadObject(Stream stream);
    }
}
