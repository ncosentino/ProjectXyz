using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Stats.Calculations;

namespace ProjectXyz.Plugins.Stats.Calculations
{
    public sealed class StatDefinitionToCalculationMappingRepositoryFacade : IReadOnlyStatDefinitionToCalculationMappingRepositoryFacade
    {
        private readonly IReadOnlyCollection<IDiscoverableReadOnlyStatDefinitionToCalculationMappingRepository> _repositories;

        public StatDefinitionToCalculationMappingRepositoryFacade(IEnumerable<IDiscoverableReadOnlyStatDefinitionToCalculationMappingRepository> repositories)
        {
            _repositories = repositories.ToArray();
        }

        public IEnumerable<IStatDefinitionToCalculationMapping> GetStatDefinitionIdToCalculationMappings() =>
            _repositories.SelectMany(x => x.GetStatDefinitionIdToCalculationMappings());
    }
}
