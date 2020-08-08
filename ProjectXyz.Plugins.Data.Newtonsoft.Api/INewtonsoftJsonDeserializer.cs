using System;
using System.IO;

using Newtonsoft.Json.Linq;

using ProjectXyz.Api.Data.Serialization;

namespace ProjectXyz.Plugins.Data.Newtonsoft.Api
{
    public delegate object NewtonsoftDeserializeDelegate(
        INewtonsoftJsonDeserializer deserializer,
        Stream stream,
        Type type);

    public interface INewtonsoftJsonDeserializer : IDeserializer
    {
        TDeserializable Deserialize<TDeserializable>(JObject serializable);

        JObject ReadObject(Stream stream);
    }
}
