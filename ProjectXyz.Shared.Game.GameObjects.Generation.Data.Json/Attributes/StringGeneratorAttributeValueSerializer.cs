using System;
using System.IO;
using ProjectXyz.Shared.Game.GameObjects.Generation.Attributes;
using ProjectXyz.Api.GameObjects.Generation.Attributes;

namespace ProjectXyz.Shared.Game.GameObjects.Generation.Data.Json
{
    public sealed class StringGeneratorAttributeValueSerializer : IDiscoverableGeneratorAttributeValueSerializer
    {
        private readonly IDtoSerializer _dtoSerializer;

        public StringGeneratorAttributeValueSerializer(IDtoSerializer dtoSerializer)
        {
            _dtoSerializer = dtoSerializer;
        }

        public Type SerializableType { get; } = typeof(StringGeneratorAttributeValue);

        public Stream Serialize(IGeneratorAttributeValue generatorAttributeValue)
        {
            var stream = Serialize((StringGeneratorAttributeValue)generatorAttributeValue);
            return stream;
        }

        private Stream Serialize(StringGeneratorAttributeValue stringGeneratorAttributeValue)
        {
            var dto = new SerializableDto(
                SerializableType.FullName,
                new StringSerializableDtoValue()
                {
                    Value = stringGeneratorAttributeValue.Value,
                });
            var outStream = _dtoSerializer.Serialize(dto);
            return outStream;
        }
    }
}
