using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Stats;

namespace ProjectXyz.Plugins.Stats
{
    public sealed class StatDefinitionToTermMappingRepository : IStatDefinitionToTermMappingRepository
    {
        private static readonly Lazy<IStatDefinitionToTermMappingRepository> NONE = new Lazy<IStatDefinitionToTermMappingRepository>(
            () => new StatDefinitionToTermMappingRepository());

        private StatDefinitionToTermMappingRepository()
        {
        }

        public static IStatDefinitionToTermMappingRepository None => NONE.Value;

        public IEnumerable<IStatDefinitionToTermMapping> GetStatDefinitionIdToTermMappings() =>
            Enumerable.Empty<IStatDefinitionToTermMapping>();
    }
}
