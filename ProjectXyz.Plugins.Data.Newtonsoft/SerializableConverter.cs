using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProjectXyz.Framework.Contracts;

namespace ProjectXyz.Plugins.Data.Newtonsoft
{
    public class SerializableConverter : JsonConverter
    {
        public override bool CanWrite => true;

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
            Contract.Requires(
                value is ISerializableDto,
                $"{nameof(value)} must be of type '{typeof(ISerializableDto)}'.");
            var serializableDto = (ISerializableDto)value;

            writer.WriteStartObject();

            writer.WritePropertyName("SerializableId");
            serializer.Serialize(writer, serializableDto.SerializableId);

            writer.WritePropertyName("Data");
            serializer.Serialize(writer, serializableDto.Data);

            writer.WriteEndObject();
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

            if (!SerializableDtoDataConverterService.Instance.TryGet(
                attributeTypeId,
                out var dataConverter))
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