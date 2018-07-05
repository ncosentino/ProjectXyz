using System;
using ProjectXyz.Api.Data.Serialization;
using ProjectXyz.Api.GameObjects.Generation.Attributes;
using ProjectXyz.Framework.Contracts;
using ProjectXyz.Shared.Game.GameObjects.Generation.Attributes;

namespace ProjectXyz.Shared.Game.GameObjects.Generation.Data.Json.Attributes
{
    public sealed class StringSerializableConverter : IDiscoverableSerializableConverter
    {
        public Type DtoType { get; } = typeof(StringSerializableDtoValue);

        public Type Type { get; } = typeof(StringGeneratorAttributeValue);

        public TSerializable Convert<TSerializable>(ISerializableDtoData dto)
        {
            Contract.Requires(
                dto is StringSerializableDtoValue,
                $"'{this}' expects to only convert '{typeof(StringSerializableDtoValue)}' " +
                $"but '{dto.GetType()}' was provided.");

            var converted = Convert((StringSerializableDtoValue)dto);
            var casted = (TSerializable)converted;
            return casted;
        }

        public ISerializableDtoData ConvertBack<TSerializable>(TSerializable serializable)
        {
            Contract.Requires(
                serializable is StringGeneratorAttributeValue,
                $"'{this}' expects to only convert '{typeof(StringGeneratorAttributeValue)}' " +
                $"but '{serializable.GetType()}' was provided.");

            var converted = Convert((StringGeneratorAttributeValue)(object)serializable);
            return converted;
        }

        private IGeneratorAttributeValue Convert(StringSerializableDtoValue dto)
        {
            var attributeValue = new StringGeneratorAttributeValue(dto.Value);
            return attributeValue;
        }

        private ISerializableDtoData Convert(StringGeneratorAttributeValue serializable)
        {
            var dto = new StringSerializableDtoValue()
            {
                Value = serializable.Value,
            };
            return dto;
        }
    }
}