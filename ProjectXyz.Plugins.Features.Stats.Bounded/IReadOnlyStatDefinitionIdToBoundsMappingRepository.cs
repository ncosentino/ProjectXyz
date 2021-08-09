using System.Collections.Generic;

namespace ProjectXyz.Plugins.Features.Stats.Bounded
{
    public interface IReadOnlyStatDefinitionIdToBoundsMappingRepository
    {
        IEnumerable<IStatDefinitionIdToBoundsMapping> GetStatDefinitionIdToBoundsMappings();
    }
}
