using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.Stats.Default
{
    public sealed class InMemoryStatDefinitionToTermMappingRepository : IDiscoverableReadOnlyStatDefinitionToTermMappingRepository
    {
        private readonly IReadOnlyCollection<IStatDefinitionToTermMapping> _statDefinitionToTermMappings;

        public InMemoryStatDefinitionToTermMappingRepository(IReadOnlyDictionary<IIdentifier, string> mapping)
        {
            _statDefinitionToTermMappings = new List<IStatDefinitionToTermMapping>(mapping
                .Select(kvp => new StatDefinitionToTermMapping(kvp.Key, kvp.Value)));
        }

        public IEnumerable<IStatDefinitionToTermMapping> GetStatDefinitionIdToTermMappings() =>
            _statDefinitionToTermMappings;

        public IStatDefinitionToTermMapping GetStatDefinitionToTermMappingById(IIdentifier statDefinitionId) =>
            _statDefinitionToTermMappings.FirstOrDefault(x => x.StatDefinitionId.Equals(statDefinitionId));

        public IStatDefinitionToTermMapping GetStatDefinitionToTermMappingByTerm(string term) =>
            _statDefinitionToTermMappings.FirstOrDefault(x => x.Term.Equals(term));
    }
}
