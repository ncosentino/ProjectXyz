using System.Collections.Generic;
using ProjectXyz.Plugins.Features.BoundedStats.Api;

namespace Examples.Modules.BoundedStats
{
    public sealed class StatDefinitionIdToBoundsMappingRepository : IDiscoverableReadOnlyStatDefinitionIdToBoundsMappingRepository
    {
        public IEnumerable<IStatDefinitionIdToBoundsMapping> GetStatDefinitionIdToBoundsMappings()
        {
            yield break;
        }
    }
}