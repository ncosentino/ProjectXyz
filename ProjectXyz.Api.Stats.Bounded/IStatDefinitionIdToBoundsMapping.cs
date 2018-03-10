using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Api.Stats.Bounded
{
    public interface IStatDefinitionIdToBoundsMapping
    {
        IIdentifier StatDefinitiondId { get; }

        IStatBounds StatBounds { get; }
    }
}
