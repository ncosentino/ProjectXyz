using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Stats;

namespace ProjectXyz.Plugins.Stats
{
    public sealed class StatDefinitionToTermMappingRepository : IReadOnlyStatDefinitionToTermMappingRepository
    {
        private static readonly Lazy<IReadOnlyStatDefinitionToTermMappingRepository> NONE = new Lazy<IReadOnlyStatDefinitionToTermMappingRepository>(
            () => new StatDefinitionToTermMappingRepository());

        private StatDefinitionToTermMappingRepository()
        {
        }

        public static IReadOnlyStatDefinitionToTermMappingRepository None => NONE.Value;

        public IEnumerable<IStatDefinitionToTermMapping> GetStatDefinitionIdToTermMappings() =>
            Enumerable.Empty<IStatDefinitionToTermMapping>();
    }
}
