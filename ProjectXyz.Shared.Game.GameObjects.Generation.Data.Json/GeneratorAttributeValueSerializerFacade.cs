using System;
using System.Collections.Generic;
using System.IO;
using ProjectXyz.Api.GameObjects.Generation.Attributes;

namespace ProjectXyz.Shared.Game.GameObjects.Generation.Data.Json
{
    public sealed class GeneratorAttributeValueSerializerFacade : IGeneratorAttributeValueSerializerFacade
    {
        private readonly Dictionary<Type, IGeneratorAttributeValueSerializer> _mapping;

        public GeneratorAttributeValueSerializerFacade()
        {
            _mapping = new Dictionary<Type, IGeneratorAttributeValueSerializer>();
        }

        public Stream Serialize(IGeneratorAttributeValue generatorAttributeValue)
        {
            IGeneratorAttributeValueSerializer serializer;
            if (!_mapping.TryGetValue(
                generatorAttributeValue.GetType(),
                out serializer))
            {
                throw new InvalidOperationException(
                    $"No serializer available for generator attribute value '{generatorAttributeValue}'.");
            }

            var stream = serializer.Serialize(generatorAttributeValue);
            return stream;
        }

        public void Register(
            Type type,
            IGeneratorAttributeValueSerializer generatorAttributeValueSerializer)
        {
            _mapping.Add(type, generatorAttributeValueSerializer);
        }
    }
}