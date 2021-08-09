using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Plugins.Features.Stats.Bounded.Default
{
    public sealed class ReadOnlyStatDefinitionIdToBoundsMappingRepositoryFacade : IReadOnlyStatDefinitionIdToBoundsMappingRepositoryFacade
    {
        private readonly IReadOnlyCollection<IDiscoverableReadOnlyStatDefinitionIdToBoundsMappingRepository> _repositories;

        public ReadOnlyStatDefinitionIdToBoundsMappingRepositoryFacade(IEnumerable<IDiscoverableReadOnlyStatDefinitionIdToBoundsMappingRepository> repositories)
        {
            _repositories = repositories.ToArray();
        }

        public IEnumerable<IStatDefinitionIdToBoundsMapping> GetStatDefinitionIdToBoundsMappings() =>
            _repositories.SelectMany(x => x.GetStatDefinitionIdToBoundsMappings());
    }
}