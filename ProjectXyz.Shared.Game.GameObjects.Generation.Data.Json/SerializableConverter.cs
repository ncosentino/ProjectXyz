using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProjectXyz.Api.Data.Serialization;

namespace ProjectXyz.Shared.Game.GameObjects.Generation.Data.Json
{
    public class SerializableConverter : JsonConverter
    {
        public override bool CanWrite => false;

        public override bool CanRead => true;

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(ISerializableDto);
        }

        public override void WriteJson(
            JsonWriter writer,
            object value,
            JsonSerializer serializer)
        {
            throw new NotSupportedException("Writing is not supported.");
        }

        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);
            var attributeTypeId = jsonObject["SerializableId"].Value<string>();
            var dataObject = jsonObject["Data"].Value<JObject>();

            ISerializableDtoDataConverter dataConverter;
            if (!SerializableDtoDataConverterService.Instance.TryGet(
                attributeTypeId,
                out dataConverter))
            {
                throw new InvalidOperationException(
                    $"There was no serializable data converter for '{attributeTypeId}'.");
            }

            var dataDto = dataConverter.Convert(dataObject);
            
            var generatorAttributeDto = new SerializableDto(
                attributeTypeId,
                dataDto);
            return generatorAttributeDto;
        }
    }
}