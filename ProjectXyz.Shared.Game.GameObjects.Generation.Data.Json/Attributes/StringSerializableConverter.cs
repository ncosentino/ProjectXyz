using System;
using ProjectXyz.Api.GameObjects.Generation.Attributes;
using ProjectXyz.Framework.Contracts;
using ProjectXyz.Shared.Game.GameObjects.Generation.Attributes;

namespace ProjectXyz.Shared.Game.GameObjects.Generation.Data.Json
{
    public sealed class StringSerializableConverter : IDiscoverableSerializableConverter
    {
        public Type DtoType { get; } = typeof(StringSerializableDtoValue);

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

        private IGeneratorAttributeValue Convert(StringSerializableDtoValue dto)
        {
            var attributeValue = new StringGeneratorAttributeValue(dto.Value);
            return attributeValue;
        }
    }
}