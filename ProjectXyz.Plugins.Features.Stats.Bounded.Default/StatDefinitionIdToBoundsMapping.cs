using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.Stats.Bounded.Default
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