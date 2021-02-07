using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Stats;

namespace ProjectXyz.Plugins.Stats
{
    public sealed class StatDefinitionToTermMappingRepository : IDiscoverableReadOnlyStatDefinitionToTermMappingRepository
    {
        private static readonly Lazy<IReadOnlyStatDefinitionToTermMappingRepository> NONE = new Lazy<IReadOnlyStatDefinitionToTermMappingRepository>(
            () => new StatDefinitionToTermMappingRepository());

        private StatDefinitionToTermMappingRepository()
        {
        }

        public static IReadOnlyStatDefinitionToTermMappingRepository None => NONE.Value;

        public IEnumerable<IStatDefinitionToTermMapping> GetStatDefinitionIdToTermMappings() =>
            Enumerable.Empty<IStatDefinitionToTermMapping>();

        public IStatDefinitionToTermMapping GetStatDefinitionToTermMappingById(IIdentifier statDefinitionId) => null;

        public IStatDefinitionToTermMapping GetStatDefinitionToTermMappingByTerm(string term) => null;
    }
}
