using System.Collections.Generic;
using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.BoundedStats;
using ProjectXyz.Plugins.Features.BoundedStats.Api;

namespace ProjectXyz.Game.Tests.Functional.TestingData.Stats
{
    public sealed class StatDefinitionIdToBoundsMappingRepository : IDiscoverableReadOnlyStatDefinitionIdToBoundsMappingRepository
    {
        private readonly StatInfo _statInfo;

        public StatDefinitionIdToBoundsMappingRepository(TestData testData)
        {
            _statInfo = testData.Stats;
        }

        public IEnumerable<IStatDefinitionIdToBoundsMapping> GetStatDefinitionIdToBoundsMappings()
        {
            yield return new StatDefinitionIdToBoundsMapping()
            {
                StatDefinitiondId = _statInfo.DefinitionIds.StatC,
                StatBounds = new StatBounds("5", "10")
            };
        }
    }

    public sealed class StatDefinitionIdToBoundsMapping : IStatDefinitionIdToBoundsMapping
    {
        public IIdentifier StatDefinitiondId { get; set; }
        public IStatBounds StatBounds { get; set; }
    }
}