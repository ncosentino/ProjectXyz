using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Stats.Calculations;

namespace ProjectXyz.Plugins.Stats.Calculations
{
    public sealed class StatDefinitionToCalculationConverter : IStatDefinitionToCalculationConverter
    {
        private readonly IReadOnlyDictionary<IIdentifier, string> _mapping;

        public StatDefinitionToCalculationConverter(IEnumerable<IStatDefinitionToCalculationMappingRepository> statDefinitionToCalculationMappingRepositories)
            : this(statDefinitionToCalculationMappingRepositories
                .SelectMany(x => x.GetStatDefinitionIdToCalculationMappings())
                .ToDictionary(x => x.StatDefinitionId, x => x.Calculation))
        {
        }

        private StatDefinitionToCalculationConverter(IReadOnlyDictionary<IIdentifier, string> mapping)
        {
            _mapping = mapping.ToDictionary(x => x.Key, x => x.Value);
        }

        public string this[IIdentifier key] => _mapping[key];

        public IEnumerable<IIdentifier> Keys => _mapping.Keys;

        public IEnumerable<string> Values => _mapping.Values;

        public int Count => _mapping.Count;

        public static IStatDefinitionToCalculationConverter FromMapping(IReadOnlyDictionary<IIdentifier, string> mapping) =>
            new StatDefinitionToCalculationConverter(mapping);

        public bool ContainsKey(IIdentifier key) => _mapping.ContainsKey(key);

        public IEnumerator<KeyValuePair<IIdentifier, string>> GetEnumerator() => _mapping.GetEnumerator();

        public bool TryGetValue(IIdentifier key, out string value) => _mapping.TryGetValue(key, out value);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
