﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using ProjectXyz.Api.Data.Databases;
using ProjectXyz.Api.Data.Serialization;
using ProjectXyz.Api.Enchantments.Generation;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Api.GameObjects.Generation.Attributes;

namespace ProjectXyz.Plugins.Features.Enchantments.Generation.MySql
{
    public sealed class EnchantmentDefinitionRepository : IEnchantmentDefinitionRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly IDeserializer _deserializer;
        private readonly IAttributeFilterer _attributeFilterer;
        private readonly Lazy<IReadOnlyCollection<IEnchantmentDefinition>> _lazyDefinitionCache;

        public EnchantmentDefinitionRepository(
            IConnectionFactory connectionFactory,
            IDeserializer deserializer,
            IAttributeFilterer attributeFilterer)
        {
            _connectionFactory = connectionFactory;
            _deserializer = deserializer;
            _attributeFilterer = attributeFilterer;
            _lazyDefinitionCache = new Lazy<IReadOnlyCollection<IEnchantmentDefinition>>(() =>
                ReadAllEnchantmentDefinitions().ToArray());
        }

        public IEnumerable<IEnchantmentDefinition> LoadEnchantmentDefinitions(IGeneratorContext generatorContext)
        {
            var enchantmentDefinitions = _lazyDefinitionCache.Value;
            var filteredEnchantmentDefinitions = _attributeFilterer.Filter(
                enchantmentDefinitions,
                generatorContext);
            foreach (var filteredEnchantmentDefinition in filteredEnchantmentDefinitions)
            {
                // TODO: ensure we have all of the Enchantment generation components
                // NOTE: this includes:
                // - Fixed ones attached to the Enchantment definition
                // - Filter-applies ones that aren't attached to the Enchantment definition but can be applied by filter requirement being met   

                yield return filteredEnchantmentDefinition;
            }
        }

        private IEnumerable<IEnchantmentDefinition> ReadAllEnchantmentDefinitions()
        {
            using (var connection = _connectionFactory.OpenNew())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
                SELECT
                    id,
                    serialized
                FROM
                    enchantment_definitions
                ;";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var raw = reader.GetString(reader.GetOrdinal("serialized"));
                        using (var dataStream = raw.ToStream(Encoding.UTF8))
                        {
                            var value = _deserializer.Deserialize<IEnchantmentDefinition>(dataStream);
                            yield return value;
                        }
                    }
                }
            }
        }
    }

    public static class StringExtensions
    {
        public static Stream ToStream(this string str, Encoding encoding)
        {
            var stream = new MemoryStream(encoding.GetBytes(str));
            stream.Position = 0;
            return stream;
        }
    }
}