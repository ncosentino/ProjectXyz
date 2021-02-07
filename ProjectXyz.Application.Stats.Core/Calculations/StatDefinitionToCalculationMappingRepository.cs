using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Stats.Calculations;

namespace ProjectXyz.Plugins.Stats.Calculations
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
