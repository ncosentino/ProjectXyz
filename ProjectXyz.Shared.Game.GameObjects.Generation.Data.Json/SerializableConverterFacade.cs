using System;
using System.Collections.Generic;
using ProjectXyz.Api.Data.Serialization;

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
            if (!_mapping.TryGetValue(
                dto.GetType(),
                out var converter))
            {
                throw new InvalidOperationException(
                    $"No converter was able to handle DTO type '{dto.GetType()}'.");
            }

            var converted = converter.Convert<TSerializable>(dto);
            return converted;
        }

        public ISerializableDtoData ConvertBack<TSerializable>(
            TSerializable serializable,
            out string serializableId)
        {
            if (!_mapping.TryGetValue(
                serializable.GetType(),
                out var converter))
            {
                throw new InvalidOperationException(
                    $"No converter was able to handle serializable type '{serializable.GetType()}'.");
            }

            var converted = converter.ConvertBack(
                serializable,
                out serializableId);
            return converted;
        }

        public void Register(
            Type type,
            Type dtoType,
            ISerializableConverter converter)
        {
            _mapping.Add(type, converter);
            _mapping.Add(dtoType, converter);
        }
    }
}