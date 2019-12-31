using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Stats.Calculations;

namespace ProjectXyz.Plugins.Stats.Calculations
{
    public sealed class StatDefinitionToCalculationMappingRepository : IStatDefinitionToCalculationMappingRepository
    {
        private static readonly Lazy<IStatDefinitionToCalculationMappingRepository> NONE = new Lazy<IStatDefinitionToCalculationMappingRepository>(
            () => new StatDefinitionToCalculationMappingRepository());

        private StatDefinitionToCalculationMappingRepository()
        {
        }

        public static IStatDefinitionToCalculationMappingRepository None => NONE.Value;

        public IEnumerable<IStatDefinitionToCalculationMapping> GetStatDefinitionIdToCalculationMappings() =>
            Enumerable.Empty<IStatDefinitionToCalculationMapping>();
    }
}
