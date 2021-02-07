using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Stats;

namespace ProjectXyz.Plugins.Stats
{
    public sealed class StatDefinitionToTermConverter : IStatDefinitionToTermConverter
    {
        private readonly IReadOnlyDictionary<IIdentifier, string> _mapping;

        public StatDefinitionToTermConverter(IReadOnlyStatDefinitionToTermMappingRepositoryFacade statDefinitionToTermMappingRepositoryFacade)
            : this(statDefinitionToTermMappingRepositoryFacade
                .GetStatDefinitionIdToTermMappings()
                .ToDictionary(x => x.StatDefinitionId, x => x.Term))
        {
        }

        private StatDefinitionToTermConverter(IReadOnlyDictionary<IIdentifier, string> mapping)
        {
            _mapping = mapping.ToDictionary(x => x.Key, x => x.Value);
        }

        public string this[IIdentifier key] => _mapping[key];

        public IEnumerable<IIdentifier> Keys => _mapping.Keys;

        public IEnumerable<string> Values => _mapping.Values;

        public int Count => _mapping.Count;

        public static IStatDefinitionToTermConverter FromMapping(IReadOnlyDictionary<IIdentifier, string> mapping) =>
            new StatDefinitionToTermConverter(mapping);

        public bool ContainsKey(IIdentifier key) => _mapping.ContainsKey(key);

        public IEnumerator<KeyValuePair<IIdentifier, string>> GetEnumerator() => _mapping.GetEnumerator();

        public bool TryGetValue(IIdentifier key, out string value) => _mapping.TryGetValue(key, out value);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
