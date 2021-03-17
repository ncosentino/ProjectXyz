using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Stats;

namespace ProjectXyz.Plugins.Stats
{
    public sealed class StatDefinitionToTermMappingRepositoryFacade : IReadOnlyStatDefinitionToTermMappingRepositoryFacade
    {
        private readonly IReadOnlyCollection<IDiscoverableReadOnlyStatDefinitionToTermMappingRepository> _repositories;

        public StatDefinitionToTermMappingRepositoryFacade(IEnumerable<IDiscoverableReadOnlyStatDefinitionToTermMappingRepository> repositories)
        {
            _repositories = repositories.ToArray();
        }

        public IEnumerable<IStatDefinitionToTermMapping> GetStatDefinitionIdToTermMappings() =>
            _repositories.SelectMany(x => x.GetStatDefinitionIdToTermMappings());

        public IStatDefinitionToTermMapping GetStatDefinitionToTermMappingById(IIdentifier statDefinitionId) =>
            _repositories.Select(x => x.GetStatDefinitionToTermMappingById(statDefinitionId)).FirstOrDefault(x => x != null);

        public IStatDefinitionToTermMapping GetStatDefinitionToTermMappingByTerm(string term) =>
            _repositories.Select(x => x.GetStatDefinitionToTermMappingByTerm(term)).FirstOrDefault(x => x != null);
    }
}
