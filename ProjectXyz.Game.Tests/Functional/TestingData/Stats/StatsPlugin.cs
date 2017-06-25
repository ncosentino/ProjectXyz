using System.Collections.Generic;
using ProjectXyz.Api.Stats;
using ProjectXyz.Api.Stats.Plugins;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Plugins.Api;

namespace ProjectXyz.Game.Tests.Functional.TestingData.Stats
{
    public sealed class StatsPlugin : IStatPlugin
    {
        public StatsPlugin(IPluginArgs pluginArgs)
        {

        }

        public IStatDefinitionToTermMappingRepository StatDefinitionToTermMappingRepository { get; } = new StatDefinitionToTermMappingRepo();

        private sealed class StatDefinitionToTermMappingRepo : IStatDefinitionToTermMappingRepository
        {
            private readonly StatInfo _statInfo;

            public StatDefinitionToTermMappingRepo()
            {
                _statInfo = new StatInfo();
            }

            public IEnumerable<IStatDefinitionToTermMapping> GetStatDefinitionIdToTermMappings()
            {
                yield return new StatDefinitionToTermMapping() { StateDefinitionId = _statInfo.DefinitionIds.StatA, Term = "STAT_A" };
                yield return new StatDefinitionToTermMapping() { StateDefinitionId = _statInfo.DefinitionIds.StatB, Term = "STAT_B" };
                yield return new StatDefinitionToTermMapping() { StateDefinitionId = _statInfo.DefinitionIds.StatC, Term = "STAT_C" };
            }

            private sealed class StatDefinitionToTermMapping : IStatDefinitionToTermMapping
            {
                public IIdentifier StateDefinitionId { get; set; }

                public string Term { get; set; }
            }
        }
    }
}