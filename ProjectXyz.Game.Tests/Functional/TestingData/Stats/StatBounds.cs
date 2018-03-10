using System.Collections.Generic;
using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Api.Stats.Bounded;

namespace ProjectXyz.Game.Tests.Functional.TestingData.Stats
{
    public sealed class StatBounds : IStatBounds
    {
        #region Constructors
        public StatBounds(
            string minimumExpression,
            string maximumExpression)
        {
            MinimumExpression = minimumExpression;
            MaximumExpression = maximumExpression;
        }
        #endregion

        #region Properties
        public string MinimumExpression { get; }

        public string MaximumExpression { get; }
        #endregion

        #region Methods
        public static IStatBounds Max(string maximumExpression)
        {
            return new StatBounds(null, maximumExpression);
        }

        public static IStatBounds Min(string minimumExpression)
        {
            return new StatBounds(minimumExpression, null);
        }
        #endregion
    }

    public sealed class StatDefinitionIdToBoundsMappingRepository : IStatDefinitionIdToBoundsMappingRepository
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