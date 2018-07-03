using System;
using System.Collections.Generic;

namespace ProjectXyz.Shared.Game.GameObjects.Generation.Data.Json
{
    public sealed class SerializableConverterFacade : ISerializableConverterFacade
    {
        private readonly Dictionary<Type, ISerializableConverter> _mapping;

        public SerializableConverterFacade()
        {
            _mapping = new Dictionary<Type, ISerializableConverter>();
        }

        public TSerializable Convert<TSerializable>(ISerializableDtoData dto)
        {
            ISerializableConverter converter;
            if (!_mapping.TryGetValue(
                dto.GetType(),
                out converter))
            {
                throw new InvalidOperationException(
                    $"No converter was able to handle DTO type '{dto.GetType()}'.");
            }

            var converted = converter.Convert<TSerializable>(dto);
            return converted;
        }

        public void Register(
            Type dtoType,
            ISerializableConverter converter)
        {
            _mapping.Add(dtoType, converter);
        }
    }
}