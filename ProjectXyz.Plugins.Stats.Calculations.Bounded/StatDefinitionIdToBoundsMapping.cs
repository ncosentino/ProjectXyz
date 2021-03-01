
using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.BoundedStats.Api;

namespace ProjectXyz.Plugins.Features.BoundedStats
{
    public sealed class StatDefinitionIdToBoundsMapping : IStatDefinitionIdToBoundsMapping
    {
        public StatDefinitionIdToBoundsMapping(
            IIdentifier statDefinitionId,
            IStatBounds bounds)
        {
            StatDefinitiondId = statDefinitionId;
            StatBounds = bounds;
        }

        public IIdentifier StatDefinitiondId { get; }

        public IStatBounds StatBounds { get; }
    }
}