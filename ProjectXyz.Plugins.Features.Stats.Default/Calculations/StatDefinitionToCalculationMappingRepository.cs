using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Plugins.Features.Stats.Calculations;

namespace ProjectXyz.Plugins.Features.Stats.Default.Calculations
{
    public sealed class StatDefinitionToCalculationMappingRepository : IDiscoverableReadOnlyStatDefinitionToCalculationMappingRepository
    {
        private static readonly Lazy<IReadOnlyStatDefinitionToCalculationMappingRepository> NONE = new Lazy<IReadOnlyStatDefinitionToCalculationMappingRepository>(
            () => new StatDefinitionToCalculationMappingRepository());

        private StatDefinitionToCalculationMappingRepository()
        {
        }

        public static IReadOnlyStatDefinitionToCalculationMappingRepository None => NONE.Value;

        public IEnumerable<IStatDefinitionToCalculationMapping> GetStatDefinitionIdToCalculationMappings() =>
            Enumerable.Empty<IStatDefinitionToCalculationMapping>();
    }
}
