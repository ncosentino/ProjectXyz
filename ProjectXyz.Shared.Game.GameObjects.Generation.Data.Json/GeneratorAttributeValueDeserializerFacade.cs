using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace ProjectXyz.Shared.Game.GameObjects.Generation.Data.Json
{
    public sealed class Deserializer : IDeserializer
    {
        private readonly ISerializableConverterFacade _serializableConverterFacade;
        private readonly JsonSerializer _jsonSerializer;

        public Deserializer(ISerializableConverterFacade serializableConverterFacade)
        {
            _serializableConverterFacade = serializableConverterFacade;
            _jsonSerializer = new JsonSerializer();
        }

        public TDeserializable Deserialize<TDeserializable>(Stream stream)
        {
            ISerializableDto dto;
            using (var reader = new JsonTextReader(new StreamReader(stream, Encoding.UTF8, false, 4096, true)))
            {
                dto = _jsonSerializer.Deserialize<ISerializableDto>(reader);
            }

            var converted = _serializableConverterFacade.Convert<TDeserializable>(dto.Data);
            return converted;
        }
    }
}