using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.BoundedStats.Api;

namespace ProjectXyz.Plugins.Features.BoundedStats
{
    public sealed class InMemoryStatDefinitionIdToBoundsMappingRepository : IDiscoverableReadOnlyStatDefinitionIdToBoundsMappingRepository
    {
        private readonly IReadOnlyCollection<IStatDefinitionIdToBoundsMapping> _statDefinitionIdToBoundsMappings;

        public InMemoryStatDefinitionIdToBoundsMappingRepository(IReadOnlyDictionary<IIdentifier, IStatBounds> mapping)
        {
            _statDefinitionIdToBoundsMappings = new List<IStatDefinitionIdToBoundsMapping>(mapping
                .Select(kvp => new StatDefinitionIdToBoundsMapping(kvp.Key, kvp.Value)));
        }

        public IEnumerable<IStatDefinitionIdToBoundsMapping> GetStatDefinitionIdToBoundsMappings() =>
            _statDefinitionIdToBoundsMappings;            
    }
}