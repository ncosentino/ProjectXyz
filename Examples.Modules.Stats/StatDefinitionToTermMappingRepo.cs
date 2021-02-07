using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Stats;
using ProjectXyz.Shared.Framework;

namespace Examples.Modules.Stats
{
    public sealed class StatDefinitionToTermMappingRepo : IDiscoverableReadOnlyStatDefinitionToTermMappingRepository
    {
        public IEnumerable<IStatDefinitionToTermMapping> GetStatDefinitionIdToTermMappings()
        {
            yield return new StatDefinitionToTermMapping(
                new StringIdentifier("stat1"),
                "stat1");
            yield return new StatDefinitionToTermMapping(
                new StringIdentifier("stat2"),
                "stat2");
        }

        public IStatDefinitionToTermMapping GetStatDefinitionToTermMappingById(IIdentifier statDefinitionId) =>
            GetStatDefinitionIdToTermMappings().Single(x => x.StatDefinitionId.Equals(statDefinitionId));

        public IStatDefinitionToTermMapping GetStatDefinitionToTermMappingByTerm(string term) =>
            GetStatDefinitionIdToTermMappings().Single(x => x.Term.Equals(term));
    }
}