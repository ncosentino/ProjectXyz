using System.Collections.Generic;
using ProjectXyz.Api.Stats;
using ProjectXyz.Shared.Framework;

namespace Examples.Modules.Stats
{
    public sealed class StatDefinitionToTermMappingRepo : IStatDefinitionToTermMappingRepository
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
    }
}